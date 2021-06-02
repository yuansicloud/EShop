using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Stores.Stores
{
    public class StoreEto : IStore, IMultiTenant
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public string MediaResources { get; set; }

        public Guid? TenantId { get; set; }
    }
}