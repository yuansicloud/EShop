using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Baskets
{
    public class CreateUpdateStaticBasketItemEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public string BasketName { get; set; }

        public Guid IdentifierId { get; set; }

        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }

        public decimal? UnitPrice { get; set; }

        public int Quantity { get; set; }
    }
}
