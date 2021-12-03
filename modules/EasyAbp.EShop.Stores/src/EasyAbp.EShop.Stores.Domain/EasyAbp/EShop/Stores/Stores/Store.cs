using System;
using System.Net.Sockets;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Stores.Stores
{
    public class Store : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        [NotNull]
        public virtual string Name { get; protected set; }
        
        // Todo: more properties.
        
        public virtual Address Address { get; protected set; }

        public virtual string MediaResources { get; protected set; }

        public virtual bool IsRetail { get; protected set; }

        protected Store() {}

        public Store(
            Guid id,
            Guid? tenantId,
            [NotNull] string name,
            bool isRetail) : base(id)
        {
            TenantId = tenantId;
            Name = name;
            IsRetail = isRetail;
        }
    }
}