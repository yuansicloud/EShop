using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductUnit : AuditedEntity<Guid>, IProductUnit, IMultiTenant
    {
        [NotNull]
        public virtual string DisplayName { get; protected set; }

        public Guid? TenantId { get; protected set; }

        protected ProductUnit()
        {
        }

        public ProductUnit(
            Guid id,
            string displayName
        ) : base(id)
        {
            DisplayName = displayName;
        }
    }
}
