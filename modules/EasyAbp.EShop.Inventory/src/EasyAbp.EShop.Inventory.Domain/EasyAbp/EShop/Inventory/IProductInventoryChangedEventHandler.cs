using EasyAbp.EShop.Products.ProductInventories;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Inventory.EasyAbp.EShop.Inventory
{
    public interface IProductInventoryChangedEventHandler : IDistributedEventHandler<ProductInventoryChangedEto>
    {
    }
}
