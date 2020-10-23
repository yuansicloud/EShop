using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public class ProductInventorySynchronizer :
        ILocalEventHandler<EntityDeletingEventData<Product>>,
        ILocalEventHandler<EntityUpdatingEventData<Product>>,
        ITransientDependency
    {
        private readonly IProductInventoryRepository _productInventoryRepository;

        public ProductInventorySynchronizer(IProductInventoryRepository productInventoryRepository)
        {
            _productInventoryRepository = productInventoryRepository;
        }
        
        [UnitOfWork(true)]
        public async Task HandleEventAsync(EntityDeletingEventData<Product> eventData)
        {
            await _productInventoryRepository.DeleteAsync(x => x.ProductId == eventData.Entity.Id);
        }

        [UnitOfWork(true)]
        public async Task HandleEventAsync(EntityUpdatingEventData<Product> eventData)
        {
            var skuIds = eventData.Entity.ProductSkus.Select(x => x.Id).ToList();

            await _productInventoryRepository.DeleteAsync(x =>
                x.ProductId == eventData.Entity.Id && !skuIds.Contains(x.ProductSkuId));
        }
    }
}