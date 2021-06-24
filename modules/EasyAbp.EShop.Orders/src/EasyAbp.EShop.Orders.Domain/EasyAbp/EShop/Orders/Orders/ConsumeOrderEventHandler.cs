using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class ConsumeOrderEventHandler : IDistributedEventHandler<ConsumeOrderEto>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _orderRepository;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IObjectMapper _objectMapper;
        public ConsumeOrderEventHandler(
            ICurrentTenant currentTenant,
            IDistributedEventBus distributedEventBus,
            IOrderManager orderManager,
            IObjectMapper objectMapper,
            IOrderRepository orderRepository)
        {
            _currentTenant = currentTenant;
            _orderManager = orderManager;
            _orderRepository = orderRepository;
            _distributedEventBus = distributedEventBus;
            _objectMapper = objectMapper;
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(ConsumeOrderEto eventData)
        {
            try
            {
                using var currentTenant = _currentTenant.Change(eventData.TenantId);

                var order = await _orderRepository.GetAsync(eventData.OrderId);

                if (order.ReducedInventoryAfterConsumingTime.HasValue || order.OrderStatus == OrderStatus.Canceled)
                {
                    throw new OrderIsInWrongStageException(order.Id);
                }

                await _distributedEventBus.PublishAsync(new OrderConsumedEto(_objectMapper.Map<Order, OrderEto>(order)));


            }
            catch { }
        }
    }
}