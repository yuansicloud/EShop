using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public class Combination : FullAuditedAggregateRoot<Guid>, ICombination, IMultiTenant
    {
        public virtual Guid CombinationDetailId { get; protected set; }

        public virtual string UniqueName { get; protected set; }

        public virtual string DisplayName { get; protected set; }

        public virtual string MediaResources { get; protected set; }

        public virtual int DisplayOrder { get; protected set; }

        public virtual bool IsPublished { get; protected set; }

        public virtual Guid StoreId { get; protected set; }

        public virtual List<CombinationItem> CombinationItems { get; protected set; }

        public virtual Guid? TenantId { get; protected set; }

        protected Combination()
        {
        }

        public Combination(
            Guid id,
            Guid combinationDetailId,
            string uniqueName,
            string displayName,
            string mediaResources,
            int displayOrder,
            bool isPublished,
            Guid storeId,
            Guid? tenantId
        ) : base(id)
        {
            CombinationDetailId = combinationDetailId;
            UniqueName = uniqueName;
            DisplayName = displayName;
            MediaResources = mediaResources;
            DisplayOrder = displayOrder;
            IsPublished = isPublished;
            StoreId = storeId;
            TenantId = tenantId;
            CombinationItems = new List<CombinationItem>();
        }

        public void TrimUniqueName()
        {
            UniqueName = UniqueName?.Trim();
        }
        public void TogglePublished(bool isPublished)
        {
            IsPublished = isPublished;
        }

    }
}
