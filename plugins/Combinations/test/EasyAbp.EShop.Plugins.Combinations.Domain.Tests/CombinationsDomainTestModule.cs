using EasyAbp.EShop.Plugins.Combinations.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Combinations;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(CombinationsEntityFrameworkCoreTestModule)
    )]
public class CombinationsDomainTestModule : AbpModule
{

}
