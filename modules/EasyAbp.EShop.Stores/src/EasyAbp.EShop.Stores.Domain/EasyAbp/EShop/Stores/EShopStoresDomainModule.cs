using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
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

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<EShopStoresDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<StoresDomainAutoMapperProfile>(validate: true);
            });
        }
    }
}