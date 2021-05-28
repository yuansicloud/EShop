namespace EasyAbp.EShop.Stores.StoreOwners
{
    using EasyAbp.EShop.Stores.StoreOwners.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 店铺管理园控制器
    /// </summary>
    [RemoteService(Name = "EasyAbpEShopStores")]
    [Route("/api/e-shop/stores/store-owner")]
    public class StoreOwnerController : StoresController
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IStoreOwnerAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreOwnerController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IStoreOwnerAppService"/>.</param>
        public StoreOwnerController(IStoreOwnerAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{StoreOwnerDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<StoreOwnerDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="GetStoreOwnerListDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{StoreOwnerDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<StoreOwnerDto>> GetListAsync(GetStoreOwnerListDto input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input">The input<see cref="CreateUpdateStoreOwnerDto"/>.</param>
        /// <returns>The <see cref="Task{StoreOwnerDto}"/>.</returns>
        [HttpPost]
        public Task<StoreOwnerDto> CreateAsync(CreateUpdateStoreOwnerDto input)
        {
            return _service.CreateAsync(input);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="CreateUpdateStoreOwnerDto"/>.</param>
        /// <returns>The <see cref="Task{StoreOwnerDto}"/>.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task<StoreOwnerDto> UpdateAsync(Guid id, CreateUpdateStoreOwnerDto input)
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
