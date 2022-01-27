using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Combinations.MongoDB;

[ConnectionStringName(CombinationsDbProperties.ConnectionStringName)]
public class CombinationsMongoDbContext : AbpMongoDbContext, ICombinationsMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureCombinations();
    }
}
