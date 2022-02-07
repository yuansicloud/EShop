using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Plugins.Combinations;

[DependsOn(
    typeof(CombinationsApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class CombinationsHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(CombinationsApplicationContractsModule).Assembly,
            CombinationsRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CombinationsHttpApiClientModule>();
        });

    }
}
