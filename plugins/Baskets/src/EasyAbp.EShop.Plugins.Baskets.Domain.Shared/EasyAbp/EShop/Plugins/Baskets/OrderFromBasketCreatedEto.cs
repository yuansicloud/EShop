using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [Serializable]
    public class OrderFromBasketCreatedEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid OrderId { get; set; }

        public string BasketName { get; set; }

        public Guid IdentifierId { get; set; }

        public OrderFromBasketCreatedEto(Guid orderId, string basketName, Guid identifierId, Guid? tenantId)
        {
            TenantId = tenantId;
            OrderId = orderId;
            BasketName = basketName;
            IdentifierId = identifierId;
        }
    }
}