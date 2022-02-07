using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EShop.Plugins.Combinations.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using EasyAbp.EShop.Stores;

namespace EasyAbp.EShop.Plugins.Combinations
{

    [DependsOn(
        typeof(AbpValidationModule),
        typeof(EShopStoresDomainSharedModule)
    )]
    public class EShopPluginsCombinationsDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopPluginsCombinationsDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<CombinationsResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/Combinations");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Combinations", typeof(CombinationsResource));
            });
        }
    }
}