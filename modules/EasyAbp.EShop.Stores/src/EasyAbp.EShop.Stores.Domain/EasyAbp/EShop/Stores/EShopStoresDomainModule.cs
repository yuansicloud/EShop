using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Stores
{
    [DependsOn(
        typeof(EShopStoresDomainSharedModule),
        typeof(EShopOrdersDomainSharedModule)
    )]
    public class EShopStoresDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<Store, StoreEto>();

                options.AutoEventSelectors.Add<Store>();
            });
        }
    }
}