namespace EasyAbp.EShop.Products.ProductHistories
{
    using EasyAbp.EShop.Products.ProductHistories.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 商品历史控制器
    /// </summary>
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/e-shop/products/product-history")]
    public class ProductHistoryController : ProductsController, IProductHistoryAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IProductHistoryAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductHistoryController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IProductHistoryAppService"/>.</param>
        public ProductHistoryController(IProductHistoryAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductHistoryDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<ProductHistoryDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="GetProductHistoryListDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{ProductHistoryDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<ProductHistoryDto>> GetListAsync(GetProductHistoryListDto input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 按修改事件获取
        /// </summary>
        /// <param name="productId">The productId<see cref="Guid"/>.</param>
        /// <param name="modificationTime">The modificationTime<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="Task{ProductHistoryDto}"/>.</returns>
        [HttpGet]
        [Route("by-time/{productId}")]
        public Task<ProductHistoryDto> GetByTimeAsync(Guid productId, DateTime modificationTime)
        {
            return _service.GetByTimeAsync(productId, modificationTime);
        }
    }
}
