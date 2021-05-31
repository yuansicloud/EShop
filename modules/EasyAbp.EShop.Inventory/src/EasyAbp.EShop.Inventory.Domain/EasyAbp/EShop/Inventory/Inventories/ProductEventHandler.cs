using EasyAbp.EShop.Inventory.Stocks;
using EasyAbp.EShop.Inventory.Warehouses;
using EasyAbp.EShop.Products.Products;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Inventory.EasyAbp.EShop.Inventory.Inventories
{
    public class ProductEventHandler : IDistributedEventHandler<ProductEto>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IStockRepository _stockRepository;
        private readonly IWarehouseManager _warehouseManager;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IStockManager _stockManager;

        public ProductEventHandler(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IStockRepository stockRepository,
            IStockManager stockManager,
            IWarehouseManager warehouseManager)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _stockRepository = stockRepository;
            _warehouseManager = warehouseManager;
            _stockManager = stockManager;
        }

        public virtual async Task HandleEventAsync(ProductEto eventData)
        {
            var warehouse = await _warehouseManager.GetDefaultWarehouse(eventData.StoreId);

            foreach (var sku in eventData.ProductSkus)
            {
                var exist = _stockRepository.Where(s => s.ProductSkuId == sku.Id).Count() > 0;

                if (exist)
                {
                    continue;
                }

                var stock = new Stock(_guidGenerator.Create(), 0, sku.Id, eventData.Id, 0, 0, true, "系统初始化自动创建存品", warehouse.Id, eventData.StoreId, _currentTenant.Id);

                await _stockManager.CreateAsync(stock);
            }
        }
    }
}