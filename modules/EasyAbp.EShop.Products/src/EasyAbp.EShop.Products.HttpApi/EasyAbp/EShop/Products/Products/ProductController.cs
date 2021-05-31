namespace EasyAbp.EShop.Products.Products
{
    using EasyAbp.EShop.Products.Products.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 商品控制器
    /// </summary>
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/e-shop/products/product")]
    public class ProductController : ProductsController, IProductAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IProductAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IProductAppService"/>.</param>
        public ProductController(IProductAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="GetProductListInput"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{ProductDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListInput input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 创建商品SPU
        /// </summary>
        /// <param name="input">The input<see cref="CreateUpdateProductDto"/>.</param>
        /// <returns>The <see cref="Task{ProductDto}"/>.</returns>
        [HttpPost]
        public Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            return _service.CreateAsync(input);
        }

        /// <summary>
        /// 编辑商品SPU
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="CreateUpdateProductDto"/>.</param>
        /// <returns>The <see cref="Task{ProductDto}"/>.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除商品SPU
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        /// <summary>
        /// 创建商品SKU
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="CreateProductSkuDto"/>.</param>
        /// <returns>The <see cref="Task{ProductDto}"/>.</returns>
        [HttpPost]
        [Route("{id}/sku")]
        public Task<ProductDto> CreateSkuAsync(Guid id, CreateProductSkuDto input)
        {
            return _service.CreateSkuAsync(id, input);
        }

        /// <summary>
        /// 编辑商品SKU
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="productSkuId">The productSkuId<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="UpdateProductSkuDto"/>.</param>
        /// <returns>The <see cref="Task{ProductDto}"/>.</returns>
        [HttpPut]
        [Route("{id}/sku/{productSkuId}")]
        public Task<ProductDto> UpdateSkuAsync(Guid id, Guid productSkuId, UpdateProductSkuDto input)
        {
            return _service.UpdateSkuAsync(id, productSkuId, input);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<ProductDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 用代码获取
        /// </summary>
        /// <param name="storeId">The storeId<see cref="Guid"/>.</param>
        /// <param name="code">The code<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{ProductDto}"/>.</returns>
        [HttpGet]
        [Route("by-code/{code}")]
        public Task<ProductDto> GetByCodeAsync(Guid storeId, string code)
        {
            return _service.GetByCodeAsync(storeId, code);
        }

        /// <summary>
        /// 删除商品SKU
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="productSkuId">The productSkuId<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductDto}"/>.</returns>
        [HttpDelete]
        [Route("{id}/sku/{productSkuId}")]
        public Task<ProductDto> DeleteSkuAsync(Guid id, Guid productSkuId)
        {
            return _service.DeleteSkuAsync(id, productSkuId);
        }

        /// <summary>
        /// 获取商品组
        /// </summary>
        /// <returns>The <see cref="Task{ListResultDto{ProductGroupDto}}"/>.</returns>
        [HttpGet]
        [Route("product-group")]
        public Task<ListResultDto<ProductGroupDto>> GetProductGroupListAsync()
        {
            return _service.GetProductGroupListAsync();
        }

        /// <summary>
        /// 更新商品SKU列表（包含创建，编辑，删除）
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="productSkuId">The productSkuId<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="UpdateProductSkuDto"/>.</param>
        /// <returns>The <see cref="Task{ProductDto}"/>.</returns>
        [HttpPut]
        [Route("{productId}/sku/list")]
        public Task<ProductDto> UpdateSkuListAsync(Guid productId, List<CreateProductSkuDto> input)
        {
            return _service.UpdateSkuListAsync(productId, input);
        }
    }
}
