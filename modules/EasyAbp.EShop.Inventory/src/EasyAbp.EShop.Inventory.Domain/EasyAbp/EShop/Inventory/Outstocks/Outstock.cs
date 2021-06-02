using EasyAbp.EShop.Inventory.Warehouses;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Inventory.Outstocks
{
    public class Outstock : FullAuditedEntity<Guid>, IOutstock, IMultiWarehouse, IMultiTenant
    {
        public DateTime OutstockTime { get; protected set; }

        public Guid ProductSkuId { get; protected set; }

        public decimal UnitPrice { get; protected set; }

        public int Units { get; protected set; }

        public string OperatorName { get; protected set; }

        public string Description { get; protected set; }

        public Guid WarehouseId { get; protected set; }

        public Guid StoreId { get; protected set; }

        public Guid? TenantId { get; protected set; }

        public Guid ProductId { get; protected set; }

        public OutstockType OutstockType { get; protected set; }

        public string OutstockNumber { get; protected set; }

        protected Outstock()
        {
        }

        public Outstock(
            Guid id,
            DateTime outstockTime,
            Guid productSkuId,
            Guid productId,
            decimal unitPrice,
            int units,
            string operatorName,
            string description,
            Guid warehouseId,
            Guid storeId,
            OutstockType outstockType,
            Guid? tenantId
        ) : base(id)
        {
            OutstockTime = outstockTime;
            ProductSkuId = productSkuId;
            ProductId = productId;
            UnitPrice = unitPrice;
            Units = units;
            OperatorName = operatorName;
            Description = description;
            WarehouseId = warehouseId;
            StoreId = storeId;
            OutstockType = outstockType;
            TenantId = tenantId;
        }

        public void SetOutstockNumber(string outstockNumber)
        {
            OutstockNumber = outstockNumber;
        }
    }
}
