using Localization.Resources.AbpUi;
using EasyAbp.EShop.Plugins.Combinations.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Plugins.Combinations;

[DependsOn(
    typeof(CombinationsApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class CombinationsHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CombinationsHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<CombinationsResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
