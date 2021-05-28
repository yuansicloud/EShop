namespace EasyAbp.EShop.Products.ProductDetails
{
    using EasyAbp.EShop.Products.ProductDetails.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 商品详情控制器
    /// </summary>
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/e-shop/products/product-detail")]
    public class ProductDetailController : ProductsController, IProductDetailAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IProductDetailAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetailController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IProductDetailAppService"/>.</param>
        public ProductDetailController(IProductDetailAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductDetailDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<ProductDetailDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="GetProductDetailListInput"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{ProductDetailDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<ProductDetailDto>> GetListAsync(GetProductDetailListInput input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input">The input<see cref="CreateUpdateProductDetailDto"/>.</param>
        /// <returns>The <see cref="Task{ProductDetailDto}"/>.</returns>
        [HttpPost]
        public Task<ProductDetailDto> CreateAsync(CreateUpdateProductDetailDto input)
        {
            return _service.CreateAsync(input);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="CreateUpdateProductDetailDto"/>.</param>
        /// <returns>The <see cref="Task{ProductDetailDto}"/>.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task<ProductDetailDto> UpdateAsync(Guid id, CreateUpdateProductDetailDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }
    }
}
