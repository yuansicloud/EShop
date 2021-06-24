using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{

    [Serializable]
    public class OrderConsumedEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public OrderEto Order { get; set; }

        public OrderConsumedEto(OrderEto order)
        {
            TenantId = order.TenantId;
            Order = order;
        }
    }
}
