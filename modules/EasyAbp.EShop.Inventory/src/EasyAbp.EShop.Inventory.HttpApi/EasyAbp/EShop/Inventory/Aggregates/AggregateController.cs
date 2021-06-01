namespace EasyAbp.EShop.Inventory.Aggregates
{
    using EasyAbp.EShop.Inventory.Aggregates.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 库存详情聚合控制器
    /// </summary>
    [RemoteService(Name = "EasyAbpEShopInventory")]
    [Route("/api/e-shop/inventory/aggregate")]
    public class AggregateController : InventoryController, IAggregateAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IAggregateAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IAggregateAppService"/>.</param>
        public AggregateController(IAggregateAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("product/{productId}")]
        public Task<ProductStockDetailDto> GetProductStockDetail(Guid productId, DateTime? termStartTime, DateTime? termEndTime)
        {
            return _service.GetProductStockDetail(productId, termStartTime, termEndTime);
        }
    }
}
