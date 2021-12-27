using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Outstocks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.EntityFrameworkCore
{
    [ConnectionStringName(InventoryDbProperties.ConnectionStringName)]
    public interface IInventoryDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<Instock> Instocks { get; set; }
        DbSet<Outstock> Outstocks { get; set; }
    }
}
