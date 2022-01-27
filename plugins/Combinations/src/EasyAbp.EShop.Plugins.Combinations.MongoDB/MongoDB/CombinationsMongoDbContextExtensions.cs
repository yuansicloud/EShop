using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Combinations.MongoDB;

public static class CombinationsMongoDbContextExtensions
{
    public static void ConfigureCombinations(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
