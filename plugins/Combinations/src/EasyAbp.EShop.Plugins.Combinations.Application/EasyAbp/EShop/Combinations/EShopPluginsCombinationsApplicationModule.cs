using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using EasyAbp.EShop.Products;

namespace EasyAbp.EShop.Plugins.Combinations
{

    [DependsOn(
        typeof(EShopProductsApplicationContractsModule),
        typeof(EShopPluginsCombinationsDomainModule),
        typeof(EShopPluginsCombinationsApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class EShopPluginsCombinationsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<EShopPluginsCombinationsApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EShopPluginsCombinationsApplicationModule>(validate: true);
            });
        }
    }
}