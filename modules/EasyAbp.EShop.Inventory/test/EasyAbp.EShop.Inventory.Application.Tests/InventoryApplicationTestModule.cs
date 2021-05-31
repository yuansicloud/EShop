using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Inventory
{
    [DependsOn(
        typeof(EShopInventoryApplicationModule),
        typeof(InventoryDomainTestModule)
        )]
    public class InventoryApplicationTestModule : AbpModule
    {

    }
}
