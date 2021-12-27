using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Outstocks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Inventory.EntityFrameworkCore
{
    [DependsOn(
        typeof(EShopInventoryDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class EShopInventoryEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<InventoryDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddRepository<Instock, InstockRepository>();
                options.AddRepository<Outstock, OutstockRepository>();
            });
        }
    }
}
