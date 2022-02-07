using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Combinations;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(EShopPluginsCombinationsDomainSharedModule)
)]
public class EShopPluginsCombinationsDomainModule : AbpModule
{

}
