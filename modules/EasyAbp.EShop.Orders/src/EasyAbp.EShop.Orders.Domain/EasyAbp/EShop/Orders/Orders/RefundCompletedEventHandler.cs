using EasyAbp.EShop.Payments.Refunds;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class RefundCompletedEventHandler : IDistributedEventHandler<EShopRefundCompletedEto>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<RefundCompletedEventHandler> _logger;
        public RefundCompletedEventHandler(
            ICurrentTenant currentTenant,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager,
            IDistributedEventBus distributedEventBus,
            IOrderRepository orderRepository,
            ILogger<RefundCompletedEventHandler> logger)
        {
            _currentTenant = currentTenant;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
            _distributedEventBus = distributedEventBus;
            _orderRepository = orderRepository;
            _logger = logger;
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EShopRefundCompletedEto eventData)
        {
            using var changeTenant = _currentTenant.Change(eventData.Refund.TenantId);

            foreach (var refundItem in eventData.Refund.RefundItems)
            {
                var order = await _orderRepository.FindAsync(refundItem.OrderId);

                if (order == null)
                {
                    _logger.LogCritical($"同步退款信息时未找到订单{refundItem.OrderId}");
                    continue;
                }

                foreach (var eto in refundItem.RefundItemOrderLines)
                {
                    order.Refund(eto.OrderLineId, eto.RefundedQuantity, eto.RefundAmount);
                }

                await _orderRepository.UpdateAsync(order, true);

                await _distributedEventBus.PublishAsync(
                    new OrderRefundedEto(_objectMapper.Map<Order, OrderEto>(order), eventData.Refund));
            }

        }
    }
}