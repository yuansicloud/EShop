using EasyAbp.EShop.Plugins.Combinations.Samples;
using Xunit;

namespace EasyAbp.EShop.Plugins.Combinations.MongoDB.Samples;

[Collection(MongoTestCollection.Name)]
public class SampleRepository_Tests : SampleRepository_Tests<CombinationsMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}
