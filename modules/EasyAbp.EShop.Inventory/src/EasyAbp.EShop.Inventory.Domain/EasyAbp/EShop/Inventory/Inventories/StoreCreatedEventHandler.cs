using EasyAbp.EShop.Inventory.Stocks;
using EasyAbp.EShop.Inventory.Warehouses;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Inventory.Inventories
{
    public class StoreCreatedEventHandler : IDistributedEventHandler<EntityCreatedEto<StoreEto>>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IStockRepository _stockRepository;
        private readonly IWarehouseManager _warehouseManager;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IStockManager _stockManager;
        private readonly ILogger<StoreCreatedEventHandler> _logger;

        public StoreCreatedEventHandler(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IStockRepository stockRepository,
            IStockManager stockManager,
            ILogger<StoreCreatedEventHandler> logger,
            IWarehouseManager warehouseManager)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _stockRepository = stockRepository;
            _warehouseManager = warehouseManager;
            _stockManager = stockManager;
            _logger = logger;
        }

        public virtual async Task HandleEventAsync(EntityCreatedEto<StoreEto> eventData)
        {
            _logger.LogInformation("收到店铺创建事件", eventData);

            await _warehouseManager.GetDefaultWarehouse(eventData.Entity.Id);
        }
    }
}