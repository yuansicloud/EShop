namespace EasyAbp.EShop.Products.Products
{
    using EasyAbp.EShop.Products.Products.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/e-shop/products/product-sku")]
    public class ProductSkuController : ProductsController, IProductSkuAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IProductSkuAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductSkuController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IProductSkuAppService"/>.</param>
        public ProductSkuController(IProductSkuAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductUnitDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<ProductSkuDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="PagedAndSortedResultRequestDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{ProductSkuDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<ProductSkuDto>> GetListAsync(GetProductSkuListInput input)
        {
            return _service.GetListAsync(input);
        }
    }
}