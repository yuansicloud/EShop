using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Outstocks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Inventory
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbpDddDomainModule),
        typeof(EShopInventoryDomainSharedModule)
    )]
    public class EShopInventoryDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<EShopInventoryDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<InventoryDomainAutoMapperProfile>(validate: false);
            });

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.AutoEventSelectors.Add<Instock>();
                options.AutoEventSelectors.Add<Outstock>();
                
            });
        }
    }
}
