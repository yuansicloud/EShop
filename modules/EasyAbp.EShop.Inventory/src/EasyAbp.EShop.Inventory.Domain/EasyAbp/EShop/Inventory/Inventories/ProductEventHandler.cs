using EasyAbp.EShop.Inventory.Stocks;
using EasyAbp.EShop.Inventory.Warehouses;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Inventory.EasyAbp.EShop.Inventory.Inventories
{
    public class ProductEventHandler : IDistributedEventHandler<EntityUpdatedEto<ProductEto>>, IDistributedEventHandler<EntityCreatedEto<ProductEto>>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IStockRepository _stockRepository;
        private readonly IWarehouseManager _warehouseManager;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IStockManager _stockManager;
        private readonly ILogger<ProductEventHandler> _logger;
        public ProductEventHandler(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IStockRepository stockRepository,
            IStockManager stockManager,
            ILogger<ProductEventHandler> logger,
            IWarehouseManager warehouseManager)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _stockRepository = stockRepository;
            _warehouseManager = warehouseManager;
            _stockManager = stockManager;
            _logger = logger;
        }

        public virtual async Task HandleEventAsync(EntityUpdatedEto<ProductEto> eventData)
        {
            _logger.LogInformation("收到商品变动事件", eventData);

            var warehouse = await _warehouseManager.GetDefaultWarehouse(eventData.Entity.StoreId);

            foreach (var sku in eventData.Entity.ProductSkus)
            {
                var exist = _stockRepository.Where(s => s.ProductSkuId == sku.Id).Count() > 0;

                if (exist || eventData.Entity.InventoryStrategy == InventoryStrategy.NoNeed)
                {
                    continue;
                }

                var stock = new Stock(_guidGenerator.Create(), 0, sku.Id, eventData.Entity.Id, 0, 0, true, "系统初始化自动创建存品", warehouse.Id, eventData.Entity.StoreId, _currentTenant.Id);

                await _stockManager.CreateAsync(stock);
            }
        }

        public virtual async Task HandleEventAsync(EntityCreatedEto<ProductEto> eventData)
        {
            _logger.LogInformation("收到商品变动事件", eventData);

            var warehouse = await _warehouseManager.GetDefaultWarehouse(eventData.Entity.StoreId);

            foreach (var sku in eventData.Entity.ProductSkus)
            {
                var exist = _stockRepository.Where(s => s.ProductSkuId == sku.Id).Count() > 0;

                if (exist || eventData.Entity.InventoryStrategy == InventoryStrategy.NoNeed)
                {
                    continue;
                }

                var stock = new Stock(_guidGenerator.Create(), 0, sku.Id, eventData.Entity.Id, 0, 0, true, "系统初始化自动创建存品", warehouse.Id, eventData.Entity.StoreId, _currentTenant.Id);

                await _stockManager.CreateAsync(stock);
            }
        }
    }
}