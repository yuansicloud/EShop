using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Outstocks;
using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Inventory.EntityFrameworkCore
{
    public static class InventoryDbContextModelCreatingExtensions
    {
        public static void ConfigureEShopInventory(
            this ModelBuilder builder,
            Action<InventoryModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new InventoryModelBuilderConfigurationOptions(
                InventoryDbProperties.DbTablePrefix,
                InventoryDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);

                b.ConfigureByConvention();

                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */

            builder.Entity<Instock>(b =>
            {
                b.ToTable(options.TablePrefix + "Instocks", options.Schema);
                b.ConfigureByConvention();

                /* Configure more properties here */

                b.Property(x => x.UnitPrice).HasColumnType("decimal(20,8)");
            });

            builder.Entity<Outstock>(b =>
            {
                b.ToTable(options.TablePrefix + "Outstocks", options.Schema);
                b.ConfigureByConvention();

                /* Configure more properties here */

                b.Property(x => x.UnitPrice).HasColumnType("decimal(20,8)");
            });

        }
    }
}
