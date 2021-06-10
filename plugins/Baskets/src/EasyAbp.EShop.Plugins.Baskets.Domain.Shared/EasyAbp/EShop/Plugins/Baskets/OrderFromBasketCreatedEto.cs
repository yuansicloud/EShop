using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [Serializable]
    public class OrderFromBasketCreatedEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid OrderId { get; set; }

        public OrderFromBasketCreatedEto(Guid orderId, Guid? tenantId)
        {
            TenantId = tenantId;
            OrderId = orderId;
        }
    }
}