using JetBrains.Annotations;
using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class CancelOrderEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid OrderId { get; set; }

        [CanBeNull]
        public string CancellationReason { get; set; }

        public CancelOrderEto(Guid? tenantId, Guid orderId, [CanBeNull] string cancellationReason)
        {
            TenantId = tenantId;
            OrderId = orderId;
            CancellationReason = cancellationReason;
        }
    }
}