using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Combinations;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CombinationsHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class CombinationsConsoleApiClientModule : AbpModule
{

}
