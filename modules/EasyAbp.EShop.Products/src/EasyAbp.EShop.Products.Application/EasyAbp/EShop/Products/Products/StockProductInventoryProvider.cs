using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Outstocks;
using EasyAbp.EShop.Inventory.Stocks;
using EasyAbp.EShop.Inventory.Stocks.Dtos;
using EasyAbp.EShop.Inventory.Warehouses;
using EasyAbp.EShop.Products.ProductInventories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products
{
    public class StockProductInventoryProvider : IProductInventoryProvider, ITransientDependency
    {
        // Todo: should use IProductInventoryStore.
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IStockAppService _stockAppService;
        private readonly IInstockAppService _instockAppService;
        private readonly IOutstockAppService _outstockAppService;
        private readonly IWarehouseAppService _warehouseAppService;

        //private readonly IStockManager _stockManager;
        public StockProductInventoryProvider(
            IUnitOfWorkManager unitOfWorkManager,
            IDistributedEventBus distributedEventBus,
            IStockAppService stockAppService,
            IInstockAppService instockAppService,
            IOutstockAppService outstockAppService,
            IWarehouseAppService warehouseAppService)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _distributedEventBus = distributedEventBus;
            _stockAppService = stockAppService;
            _instockAppService = instockAppService;
            _outstockAppService = outstockAppService;
            _warehouseAppService = warehouseAppService;
        }

        public virtual async Task<InventoryDataModel> GetInventoryDataAsync(Product product, ProductSku productSku)
        {
            var stocks = await _stockAppService.GetListAsync(new GetStockListInput
            {
                MaxResultCount = int.MaxValue,
                ProductId = product.Id,
                ProductSkuId = productSku.Id
            });

            return ConvertToInventoryDataModel(stocks.Items);
        }

        public virtual async Task<Dictionary<Guid, InventoryDataModel>> GetInventoryDataDictionaryAsync(Product product)
        {
            var stocks = await _stockAppService.GetListAsync(new GetStockListInput
            {
                MaxResultCount = int.MaxValue,
                ProductId = product.Id
            });

            var dict = stocks.Items.GroupBy(s => s.ProductSkuId).ToDictionary(k => k.Key, k => new InventoryDataModel
            {
                Inventory = k.Sum(s => s.Quantity - s.LockedQuantity)
            });

            foreach (var sku in product.ProductSkus)
            {
                dict.GetOrAdd(sku.Id, () => new InventoryDataModel());
            }

            return dict;
        }

        public virtual async Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, int quantity, bool decreaseSold)
        {
            var stocks = await _stockAppService.GetListAsync(new GetStockListInput
            {
                MaxResultCount = int.MaxValue,
                ProductId = product.Id,
                ProductSkuId = productSku.Id
            });

            var addStock = stocks.Items.OrderBy(s => s.DisplayOrder).FirstOrDefault();

            if (addStock is null)
            {
                return false;
            }

            var inventory = ConvertToInventoryDataModel(stocks.Items).Inventory;

            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            await _instockAppService.CreateAsync(new Inventory.Instocks.Dtos.InstockCreateDto
            {
                InstockTime = DateTime.Now,
                InstockType = decreaseSold ? InstockType.OrderRefund : InstockType.Other,
                OperatorName = "系统自动",
                ProductId = product.Id,
                ProductSkuId = productSku.Id,
                StoreId = addStock.StoreId,
                WarehouseId = addStock.WarehouseId,
                UnitPrice = 0,
                Units = quantity
            });

            PublishInventoryChangedEventOnUowCompleted(uow, product.TenantId, product.StoreId,
                addStock.ProductId, addStock.ProductSkuId, inventory,
                inventory + quantity, 0);

            await uow.CompleteAsync();

            return true;
        }

        public virtual async Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, int quantity, bool increaseSold)
        {
            {
                var stocks = await _stockAppService.GetListAsync(new GetStockListInput
                {
                    MaxResultCount = int.MaxValue,
                    ProductId = product.Id,
                    ProductSkuId = productSku.Id
                });

                var addStock = stocks.Items.OrderBy(s => s.DisplayOrder).FirstOrDefault();

                if (addStock is null)
                {
                    return false;
                }

                var inventory = ConvertToInventoryDataModel(stocks.Items).Inventory;

                using var uow = _unitOfWorkManager.Begin(isTransactional: true);

                await _outstockAppService.CreateAsync(new Inventory.Outstocks.Dtos.OutstockCreateDto
                {
                    OutstockTime = DateTime.Now,
                    OutstockType = increaseSold ? OutstockType.Sale : OutstockType.Other,
                    OperatorName = "系统自动",
                    ProductId = product.Id,
                    ProductSkuId = productSku.Id,
                    StoreId = addStock.StoreId,
                    WarehouseId = addStock.WarehouseId,
                    UnitPrice = productSku.Price,
                    Units = quantity
                });

                PublishInventoryChangedEventOnUowCompleted(uow, product.TenantId, product.StoreId,
                    addStock.ProductId, addStock.ProductSkuId, inventory,
                    inventory + quantity, 0);

                await uow.CompleteAsync();

                return true;
            }
        }

        protected virtual void PublishInventoryChangedEventOnUowCompleted(IUnitOfWork uow, Guid? tenantId, Guid storeId,
            Guid productId, Guid productSkuId, int originalInventory, int newInventory, long sold)
        {
            uow.OnCompleted(async () => await _distributedEventBus.PublishAsync(new ProductInventoryChangedEto(
                tenantId,
                storeId,
                productId,
                productSkuId,
                originalInventory,
                newInventory,
                sold)));
        }

        protected virtual InventoryDataModel ConvertToInventoryDataModel(IEnumerable<StockDto> models)
        {
            if (models is null)
            {
                return new InventoryDataModel();
            }

            return new InventoryDataModel
            {
                Inventory = models.Sum(m => m.Quantity - m.LockedQuantity)
            };
        }
    }
}