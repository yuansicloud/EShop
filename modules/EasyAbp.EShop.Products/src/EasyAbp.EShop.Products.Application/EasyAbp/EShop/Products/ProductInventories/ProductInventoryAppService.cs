using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.ProductInventories.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Stores.Authorization;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public class ProductInventoryAppService : ApplicationService, IProductInventoryAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductInventoryRepository _repository;
        private readonly StockProductInventoryProvider _productInventoryProvider;

        public ProductInventoryAppService(
            IProductRepository productRepository,
            IProductInventoryRepository repository,
            StockProductInventoryProvider productInventoryProvider)
        {
            _productRepository = productRepository;
            _repository = repository;
            _productInventoryProvider = productInventoryProvider;
        }

        [Authorize(ProductsPermissions.ProductInventory.Default)]
        public virtual async Task<ProductInventoryDto> GetAsync(Guid productId, Guid productSkuId)
        {
            throw new UserFriendlyException("无法调用的方法");
            //var productInventory = await _repository.FindAsync(x => x.ProductSkuId == productSkuId);

            //if (productInventory == null)
            //{
            //    productInventory = new ProductInventory(GuidGenerator.Create(), CurrentTenant.Id, productId,
            //        productSkuId, 0, 0);

            //    await _repository.InsertAsync(productInventory, true);
            //}

            //return ObjectMapper.Map<ProductInventory, ProductInventoryDto>(productInventory);
        }

        public virtual async Task<ProductInventoryDto> UpdateAsync(UpdateProductInventoryDto input)
        {
            throw new UserFriendlyException("无法调用的方法");
            //var product = await _productRepository.GetAsync(input.ProductId);

            //await AuthorizationService.CheckMultiStorePolicyAsync(product.StoreId,
            //    ProductsPermissions.ProductInventory.Update, ProductsPermissions.ProductInventory.CrossStore);

            //var productInventory = await _repository.FindAsync(x => x.ProductSkuId == input.ProductSkuId);

            //if (productInventory == null)
            //{
            //    productInventory =
            //        new ProductInventory(GuidGenerator.Create(), CurrentTenant.Id, input.ProductId, input.ProductSkuId,
            //            0, 0);

            //    await _repository.InsertAsync(productInventory, true);
            //}

            //await ChangeInventoryAsync(product, productInventory, input.ChangedInventory);

            //return ObjectMapper.Map<ProductInventory, ProductInventoryDto>(productInventory);
        }

        protected virtual async Task ChangeInventoryAsync(Product product, ProductInventory productInventory,
            int changedInventory)
        {
            //if (changedInventory >= 0)
            //{
            //    if (!await _productInventoryProvider.TryIncreaseInventoryAsync(product, productInventory,
            //        changedInventory, false))
            //    {
            //        throw new InventoryChangeFailedException(productInventory.ProductId, productInventory.ProductSkuId,
            //            productInventory.Inventory, changedInventory);
            //    }
            //}
            //else
            //{
            //    if (!await _productInventoryProvider.TryReduceInventoryAsync(product, productInventory,
            //        -changedInventory, false))
            //    {
            //        throw new InventoryChangeFailedException(productInventory.ProductId, productInventory.ProductSkuId,
            //            productInventory.Inventory, changedInventory);
            //    }
            //}
        }
    }
}