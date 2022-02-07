using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Plugins.Combinations.Combinations;
using EasyAbp.EShop.Plugins.Combinations.CombinationDetails;

namespace EasyAbp.EShop.Plugins.Combinations.EntityFrameworkCore;

[ConnectionStringName(CombinationsDbProperties.ConnectionStringName)]
public class CombinationsDbContext : AbpDbContext<CombinationsDbContext>, ICombinationsDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
        public DbSet<Combination> Combinations { get; set; }
        public DbSet<CombinationItem> CombinationItems { get; set; }
        public DbSet<CombinationDetail> CombinationDetails { get; set; }

    public CombinationsDbContext(DbContextOptions<CombinationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureCombinations();
    }
}
