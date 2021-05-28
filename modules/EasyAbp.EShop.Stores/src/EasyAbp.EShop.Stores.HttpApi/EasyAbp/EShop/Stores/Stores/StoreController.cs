namespace EasyAbp.EShop.Stores.Stores
{
    using EasyAbp.EShop.Stores.Stores.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 店铺控制器
    /// </summary>
    [RemoteService(Name = "EasyAbpEShopStores")]
    [Route(StoresConsts.StoreRouteBase)]
    public class StoreController : StoresController, IStoreAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IStoreAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IStoreAppService"/>.</param>
        public StoreController(IStoreAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{StoreDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<StoreDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="GetStoreListInput"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{StoreDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<StoreDto>> GetListAsync(GetStoreListInput input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input">The input<see cref="CreateUpdateStoreDto"/>.</param>
        /// <returns>The <see cref="Task{StoreDto}"/>.</returns>
        [HttpPost]
        public Task<StoreDto> CreateAsync(CreateUpdateStoreDto input)
        {
            return _service.CreateAsync(input);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="CreateUpdateStoreDto"/>.</param>
        /// <returns>The <see cref="Task{StoreDto}"/>.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task<StoreDto> UpdateAsync(Guid id, CreateUpdateStoreDto input)
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
        /// 获取默认店铺
        /// </summary>
        /// <returns>The <see cref="Task{StoreDto}"/>.</returns>
        [HttpGet]
        [Route("default")]
        public Task<StoreDto> GetDefaultAsync()
        {
            return _service.GetDefaultAsync();
        }
    }
}
