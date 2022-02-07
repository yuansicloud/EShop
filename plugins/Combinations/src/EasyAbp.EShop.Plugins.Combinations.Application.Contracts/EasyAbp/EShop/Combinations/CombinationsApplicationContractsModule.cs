using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Plugins.Combinations;

[DependsOn(
    typeof(CombinationsDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class CombinationsApplicationContractsModule : AbpModule
{

}
