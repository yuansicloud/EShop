using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Plugins.Combinations;

[DependsOn(
    typeof(EShopPluginsCombinationsApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class EShopPluginsCombinationsHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(EShopPluginsCombinationsApplicationContractsModule).Assembly,
            CombinationsRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EShopPluginsCombinationsHttpApiClientModule>();
        });

    }
}
