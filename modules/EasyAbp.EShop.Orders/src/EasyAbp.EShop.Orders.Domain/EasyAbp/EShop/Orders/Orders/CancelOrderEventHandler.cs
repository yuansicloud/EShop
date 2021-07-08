using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class CancelOrderEventHandler : IDistributedEventHandler<CancelOrderEto>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _orderRepository;

        public CancelOrderEventHandler(
            ICurrentTenant currentTenant,
            IOrderManager orderManager,
            IOrderRepository orderRepository)
        {
            _currentTenant = currentTenant;
            _orderManager = orderManager;
            _orderRepository = orderRepository;
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(CancelOrderEto eventData)
        {
            try
            {
                using var currentTenant = _currentTenant.Change(eventData.TenantId);

                var order = await _orderRepository.GetAsync(eventData.OrderId);

                await _orderManager.CancelAsync(order, eventData.CancellationReason);
            }
            catch { }
        }
    }
}