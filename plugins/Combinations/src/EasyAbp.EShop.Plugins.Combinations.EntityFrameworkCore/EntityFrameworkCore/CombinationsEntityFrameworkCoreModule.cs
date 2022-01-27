using EasyAbp.EShop.Plugins.Combinations.Combinations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Combinations.EntityFrameworkCore;

[DependsOn(
    typeof(CombinationsDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class CombinationsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<CombinationsDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddRepository<Combination, CombinationRepository>();
                options.AddRepository<CombinationItem, CombinationItemRepository>();
        });
    }
}
