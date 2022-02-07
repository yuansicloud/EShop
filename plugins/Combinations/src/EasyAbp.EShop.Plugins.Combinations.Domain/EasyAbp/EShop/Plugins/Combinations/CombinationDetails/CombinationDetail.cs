using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Combinations.CombinationDetails
{
    public class CombinationDetail : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid? StoreId { get; protected set; }

        [CanBeNull]
        public virtual string Description { get; protected set; }

        protected CombinationDetail() { }

        public CombinationDetail(
            Guid id,
            Guid? tenantId,
            Guid? storeId,
            [CanBeNull] string description) : base(id)
        {
            TenantId = tenantId;
            StoreId = storeId;
            Description = description;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public CombinationDetail(
            Guid id,
            Guid? tenantId,
            Guid? storeId,
            string description
        ) : base(id)
        {
            TenantId = tenantId;
            StoreId = storeId;
            Description = description;
        }
    }
}
