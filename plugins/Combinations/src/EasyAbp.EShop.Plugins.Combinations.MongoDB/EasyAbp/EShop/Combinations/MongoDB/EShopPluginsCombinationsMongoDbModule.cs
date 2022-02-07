using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Combinations.MongoDB;

[DependsOn(
    typeof(EShopPluginsCombinationsDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class EShopPluginsCombinationsMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<CombinationsMongoDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
        });
    }
}
