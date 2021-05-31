using EasyAbp.EShop.Inventory.Localization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Inventory
{
    [DependsOn(
        typeof(EShopInventoryApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EShopInventoryHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopInventoryHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<InventoryResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}