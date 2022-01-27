using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Combinations;

[DependsOn(
    typeof(CombinationsApplicationModule),
    typeof(CombinationsDomainTestModule)
    )]
public class CombinationsApplicationTestModule : AbpModule
{

}
