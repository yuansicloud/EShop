using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Inventory.Instocks
{
    public class Instock : FullAuditedEntity<Guid>, IInstock,  IMultiTenant
    {
        public Guid ProductId { get; protected set; }

        public Guid ProductSkuId { get; protected set; }

        public string Description { get; protected set; }

        public decimal UnitPrice { get; protected set; }

        public DateTime InstockTime { get; protected set; }

        public string OperatorName { get; protected set; }

        public Guid StoreId { get; protected set; }


        public InstockType InstockType { get; protected set; }

        public string InstockNumber { get; protected set; }

        public string ProductGroupName { get; protected set; }

        public string ProductGroupDisplayName { get; protected set; }

        public string ProductUniqueName { get; protected set; }

        public string ProductDisplayName { get; protected set; }

        public string SkuName { get; protected set; }

        public string SkuDescription { get; protected set; }

        public string MediaResources { get; protected set; }

        public string Currency { get; protected set; }

        public int Quantity { get; protected set; }

        public string Unit { get; protected set; }

        public Guid? TenantId { get; protected set; }


        protected Instock()
        {
        }

        public void SetInstockNumber(string instockNumber)
        {
            InstockNumber = instockNumber;
        }

        public Instock(
            Guid id,
            DateTime instockTime,
            Guid productSkuId,
            string description,
            decimal unitPrice,
            string operatorName,
            Guid storeId,
            Guid? tenantId,
            Guid productId,
            InstockType instockType,
            string instockNumber,
            string productGroupName,
            string productGroupDisplayName,
            string productUniqueName,
            string productDisplayName,
            string skuName,
            string skuDescription,
            string mediaResources,
            string currency,
            int quantity,
            string unit
        ) : base(id)
        {
            InstockTime = instockTime;
            ProductSkuId = productSkuId;
            Description = description;
            UnitPrice = unitPrice;
            OperatorName = operatorName;
            StoreId = storeId;
            TenantId = tenantId;
            ProductId = productId;
            InstockType = instockType;
            InstockNumber = instockNumber;
            ProductGroupName = productGroupName;
            ProductGroupDisplayName = productGroupDisplayName;
            ProductUniqueName = productUniqueName;
            ProductDisplayName = productDisplayName;
            SkuName = skuName;
            SkuDescription = skuDescription;
            MediaResources = mediaResources;
            Currency = currency;
            Quantity = quantity;
            Unit = unit;
        }
    }
}
