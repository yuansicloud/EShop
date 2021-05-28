namespace EasyAbp.EShop.Products.Products
{
    using EasyAbp.EShop.Products.Products.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 商品显示视图控制器
    /// </summary>
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/e-shop/products/product/view")]
    public class ProductViewController : ProductsController, IProductViewAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IProductViewAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductViewController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IProductViewAppService"/>.</param>
        public ProductViewController(IProductViewAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 列表视图
        /// </summary>
        /// <param name="input">The input<see cref="GetProductListInput"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{ProductViewDto}}"/>.</returns>
        [HttpGet]
        public virtual Task<PagedResultDto<ProductViewDto>> GetListAsync(GetProductListInput input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 详情视图
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductViewDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public virtual Task<ProductViewDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }
    }
}
