namespace EasyAbp.EShop.Products.Products
{
    using EasyAbp.EShop.Products.Products.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/e-shop/products/product-unit")]
    public class ProductUnitController : ProductsController, IProductUnitAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IProductUnitAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUnitController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IProductUnitAppService"/>.</param>
        public ProductUnitController(IProductUnitAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input">The input<see cref="CreateProductUnitDto"/>.</param>
        /// <returns>The <see cref="Task{ProductUnitDto}"/>.</returns>
        [HttpPost]
        public Task<ProductUnitDto> CreateAsync(CreateUpdateProductUnitDto input)
        {
            return _service.CreateAsync(input);
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

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ProductUnitDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<ProductUnitDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="PagedAndSortedResultRequestDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{ProductUnitDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<ProductUnitDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="UpdateProductUnitDto"/>.</param>
        /// <returns>The <see cref="Task{ProductUnitDto}"/>.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task<ProductUnitDto> UpdateAsync(Guid id, CreateUpdateProductUnitDto input)
        {
            return _service.UpdateAsync(id, input);
        }
    }
}