using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Inventory.Outstocks
{
    public class Outstock : FullAuditedEntity<Guid>, IOutstock, IMultiTenant
    {
        public DateTime OutstockTime { get; protected set; }

        public Guid ProductSkuId { get; protected set; }

        public decimal UnitPrice { get; protected set; }

        public string OperatorName { get; protected set; }

        public string Description { get; protected set; }

        public Guid StoreId { get; protected set; }

        public Guid? TenantId { get; protected set; }

        public Guid ProductId { get; protected set; }

        public OutstockType OutstockType { get; protected set; }

        public string OutstockNumber { get; protected set; }

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

        protected Outstock()
        {
        }

        public void SetOutstockNumber(string outstockNumber)
        {
            OutstockNumber = outstockNumber;
        }

        public Outstock(
            Guid id,
            DateTime outstockTime,
            Guid productSkuId,
            decimal unitPrice,
            string operatorName,
            string description,
            Guid storeId,
            Guid? tenantId,
            Guid productId,
            OutstockType outstockType,
            string outstockNumber,
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
            OutstockTime = outstockTime;
            ProductSkuId = productSkuId;
            UnitPrice = unitPrice;
            OperatorName = operatorName;
            Description = description;
            StoreId = storeId;
            TenantId = tenantId;
            ProductId = productId;
            OutstockType = outstockType;
            OutstockNumber = outstockNumber;
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
