namespace EasyAbp.EShop.Products.Categories
{
    using EasyAbp.EShop.Products.Categories.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 类别控制器
    /// </summary>
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route(ProductsConsts.CategoryRouteBase)]
    public class CategoryController : ProductsController, ICategoryAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly ICategoryAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="ICategoryAppService"/>.</param>
        public CategoryController(ICategoryAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{CategoryDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<CategoryDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="GetCategoryListDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{CategoryDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input">The input<see cref="CreateUpdateCategoryDto"/>.</param>
        /// <returns>The <see cref="Task{CategoryDto}"/>.</returns>
        [HttpPost]
        public Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            return _service.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="CreateUpdateCategoryDto"/>.</param>
        /// <returns>The <see cref="Task{CategoryDto}"/>.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input)
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

        /// <summary>
        /// 简略列表
        /// </summary>
        /// <param name="input">The input<see cref="GetCategoryListDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{CategorySummaryDto}}"/>.</returns>
        [HttpGet]
        [Route("summary")]
        public async Task<PagedResultDto<CategorySummaryDto>> GetSummaryListAsync(GetCategoryListDto input)
        {
            return await _service.GetSummaryListAsync(input);
        }
    }
}
