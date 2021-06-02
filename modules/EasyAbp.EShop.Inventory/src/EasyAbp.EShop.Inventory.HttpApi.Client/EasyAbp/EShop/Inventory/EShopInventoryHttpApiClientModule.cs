using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Inventory
{
    [DependsOn(
        typeof(EShopInventoryApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EShopInventoryHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "EShopInventory";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EShopInventoryApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
