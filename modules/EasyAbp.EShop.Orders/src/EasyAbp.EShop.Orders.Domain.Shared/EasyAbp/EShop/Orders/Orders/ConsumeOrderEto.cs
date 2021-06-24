using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class ConsumeOrderEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid OrderId { get; set; }

        public ConsumeOrderEto(Guid? tenantId, Guid orderId)
        {
            TenantId = tenantId;
            OrderId = orderId;
        }
    }
}