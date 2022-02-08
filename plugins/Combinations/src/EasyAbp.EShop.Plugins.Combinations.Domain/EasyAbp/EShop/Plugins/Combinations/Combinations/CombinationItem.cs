using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public class CombinationItem : FullAuditedEntity<Guid>, IProductData, IMultiTenant
    {
        public virtual int Quantity { get; protected set; }

        public virtual string MediaResources { get; protected set; }

        public virtual string ProductUniqueName { get; protected set; }

        public virtual string ProductDisplayName { get; protected set; }

        public virtual string SkuName { get; protected set; }

        public virtual string SkuDescription { get; protected set; }

        public virtual string Currency { get; protected set; }

        public virtual decimal UnitPrice { get; protected set; }

        public virtual decimal TotalPrice { get; protected set; }

        public virtual decimal TotalDiscount { get; protected set; }

        public virtual bool IsFixedPrice { get; protected set; }

        public virtual string Unit { get; protected set; }

        public virtual Guid ProductId { get; protected set; }

        public virtual Guid ProductSkuId { get; protected set; }

        public virtual Guid? TenantId { get; protected set; }

        protected CombinationItem()
        {
        }

        public CombinationItem(
            Guid id,
            int quantity,
            string mediaResources,
            string productUniqueName,
            string productDisplayName,
            string skuName,
            string skuDescription,
            string currency,
            decimal unitPrice,
            decimal totalPrice,
            decimal totalDiscount,
            bool isFixedPrice,
            string unit,
            Guid? tenantId
        ) : base(id)
        {
            Quantity = quantity;
            MediaResources = mediaResources;
            ProductUniqueName = productUniqueName;
            ProductDisplayName = productDisplayName;
            SkuName = skuName;
            SkuDescription = skuDescription;
            Currency = currency;
            UnitPrice = unitPrice;
            TotalPrice = totalPrice;
            TotalDiscount = totalDiscount;
            IsFixedPrice = isFixedPrice;
            Unit = unit;
            TenantId = tenantId;
        }

        public virtual void UpdateTotalPrice()
        {
            TotalPrice = UnitPrice * Quantity;

            if (TotalPrice < 0)
            {
                throw new BusinessException(null, "TotalPrice can not be less than 0");
            }
        }
    }
}
