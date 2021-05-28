namespace EasyAbp.EShop.Products.ProductInventories
{
    using EasyAbp.EShop.Products.ProductInventories.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;

    /// <summary>
    /// 商品库存控制器
    /// </summary>
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/e-shop/products/product-inventory")]
    public class ProductInventoryController : ProductsController, IProductInventoryAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IProductInventoryAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductInventoryController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IProductInventoryAppService"/>.</param>
        public ProductInventoryController(IProductInventoryAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="productId">The productId<see cref="Guid"/>.</param>
        /// <param name="productSkuId">The productSkuId<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductInventoryDto}"/>.</returns>
        [HttpGet]
        public Task<ProductInventoryDto> GetAsync(Guid productId, Guid productSkuId)
        {
            return _service.GetAsync(productId, productSkuId);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="input">The input<see cref="UpdateProductInventoryDto"/>.</param>
        /// <returns>The <see cref="Task{ProductInventoryDto}"/>.</returns>
        [HttpPut]
        public Task<ProductInventoryDto> UpdateAsync(UpdateProductInventoryDto input)
        {
            return _service.UpdateAsync(input);
        }
    }
}
