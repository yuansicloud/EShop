using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Application.Dtos;
using EasyAbp.EShop.Stores;

namespace EasyAbp.EShop.Inventory
{
    [DependsOn(
        typeof(EShopInventoryDomainModule),
        typeof(EShopInventoryApplicationContractsModule),
        typeof(EShopStoresApplicationSharedModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class EShopInventoryApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            LimitedResultRequestDto.MaxMaxResultCount = int.MaxValue;

            context.Services.AddAutoMapperObjectMapper<EShopInventoryApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EShopInventoryApplicationModule>(validate: true);
            });
        }
    }
}
