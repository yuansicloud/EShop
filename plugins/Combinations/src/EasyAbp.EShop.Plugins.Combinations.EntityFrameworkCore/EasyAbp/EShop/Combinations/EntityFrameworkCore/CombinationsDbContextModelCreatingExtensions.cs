using EasyAbp.EShop.Plugins.Combinations.CombinationDetails;
using EasyAbp.EShop.Plugins.Combinations.Combinations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Plugins.Combinations.EntityFrameworkCore;

public static class CombinationsDbContextModelCreatingExtensions
{
    public static void ConfigureCombinations(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(CombinationsDbProperties.DbTablePrefix + "Questions", CombinationsDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */


        builder.Entity<Combination>(b =>
        {
            b.ToTable(CombinationsDbProperties.DbTablePrefix + "Combinations", CombinationsDbProperties.DbSchema);
            b.ConfigureByConvention();


            /* Configure more properties here */
            b.HasIndex(x => x.UniqueName);
        });


        builder.Entity<CombinationItem>(b =>
        {
            b.ToTable(CombinationsDbProperties.DbTablePrefix + "CombinationItems", CombinationsDbProperties.DbSchema);
            b.ConfigureByConvention();


            /* Configure more properties here */
            b.Property(x => x.TotalPrice).HasColumnType("decimal(20,8)");
            b.Property(x => x.TotalDiscount).HasColumnType("decimal(20,8)");
        });


        builder.Entity<CombinationDetail>(b =>
        {
            b.ToTable(CombinationsDbProperties.DbTablePrefix + "CombinationDetails", CombinationsDbProperties.DbSchema);
            b.ConfigureByConvention();


                /* Configure more properties here */
        });
    }
}
