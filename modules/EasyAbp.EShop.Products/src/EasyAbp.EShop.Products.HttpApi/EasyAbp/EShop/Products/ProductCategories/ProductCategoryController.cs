namespace EasyAbp.EShop.Products.ProductCategories
{
    using EasyAbp.EShop.Products.ProductCategories.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 商品类别控制器
    /// </summary>
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/e-shop/products/product-category")]
    public class ProductCategoryController : ProductsController, IProductCategoryAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IProductCategoryAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IProductCategoryAppService"/>.</param>
        public ProductCategoryController(IProductCategoryAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductCategoryDto}"/>.</returns>
        [HttpGet]
        [Route("{id}/abandoned")]
        [RemoteService(false)]
        public Task<ProductCategoryDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="GetProductCategoryListDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{ProductCategoryDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<ProductCategoryDto>> GetListAsync(GetProductCategoryListDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}
