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
                b.ToTable(options.TablePrefix + "Combinations", options.Schema);
                b.ConfigureByConvention(); 
                

                /* Configure more properties here */
            });


            builder.Entity<CombinationItem>(b =>
            {
                b.ToTable(options.TablePrefix + "CombinationItems", options.Schema);
                b.ConfigureByConvention(); 
                

                /* Configure more properties here */
            });
    }
}
