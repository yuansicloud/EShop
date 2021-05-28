namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    using EasyAbp.EShop.Products.ProductDetailHistories.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 商品详情历史记录控制器
    /// </summary>
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/e-shop/products/product-detail-history")]
    public class ProductDetailHistoryController : ProductsController, IProductDetailHistoryAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IProductDetailHistoryAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDetailHistoryController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IProductDetailHistoryAppService"/>.</param>
        public ProductDetailHistoryController(IProductDetailHistoryAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductDetailHistoryDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<ProductDetailHistoryDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="GetProductDetailHistoryListDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{ProductDetailHistoryDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<ProductDetailHistoryDto>> GetListAsync(GetProductDetailHistoryListDto input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 按修改事件获取
        /// </summary>
        /// <param name="productId">The productId<see cref="Guid"/>.</param>
        /// <param name="modificationTime">The modificationTime<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="Task{ProductDetailHistoryDto}"/>.</returns>
        [HttpGet]
        [Route("by-time/{productId}")]
        public Task<ProductDetailHistoryDto> GetByTimeAsync(Guid productId, DateTime modificationTime)
        {
            return _service.GetByTimeAsync(productId, modificationTime);
        }
    }
}
