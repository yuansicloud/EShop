using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Inventory.StockHistories
{
    public class StockHistory : CreationAuditedEntity<Guid>, IStockHistory, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual int LockedQuantity { get; protected set; }

        public virtual Guid ProductSkuId { get; protected set; }

        public virtual int Quantity { get; protected set; }

        public virtual int AdjustedQuantity { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual Guid StoreId { get; protected set; }

        public Guid ProductId { get; set; }

        protected StockHistory()
        {
        }

    }
}
