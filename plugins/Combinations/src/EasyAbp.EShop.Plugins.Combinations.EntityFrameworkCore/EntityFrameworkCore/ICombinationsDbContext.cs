using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Plugins.Combinations.Combinations;

namespace EasyAbp.EShop.Plugins.Combinations.EntityFrameworkCore;

[ConnectionStringName(CombinationsDbProperties.ConnectionStringName)]
public interface ICombinationsDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
        DbSet<Combination> Combinations { get; set; }
        DbSet<CombinationItem> CombinationItems { get; set; }
}
