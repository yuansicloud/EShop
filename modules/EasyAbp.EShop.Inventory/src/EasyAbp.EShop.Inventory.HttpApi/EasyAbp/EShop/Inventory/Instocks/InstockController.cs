namespace EasyAbp.EShop.Inventory.Instocks
{
    using EasyAbp.EShop.Inventory.Instocks.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 入库记录控制器
    /// </summary>
    [RemoteService(Name = "EasyAbpEShopInventory")]
    [Route("/api/e-shop/inventory/instock")]
    public class InstockController : InventoryController, IInstockAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IInstockAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstockController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IInstockAppService"/>.</param>
        public InstockController(IInstockAppService service)
        {
            _service = service;
        }


        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{InstockDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<InstockDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="PagedAndSortedResultRequestDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{InstockDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<InstockDto>> GetListAsync(GetInstockListInput input)
        {
            return _service.GetListAsync(input);
        }
    }
}
