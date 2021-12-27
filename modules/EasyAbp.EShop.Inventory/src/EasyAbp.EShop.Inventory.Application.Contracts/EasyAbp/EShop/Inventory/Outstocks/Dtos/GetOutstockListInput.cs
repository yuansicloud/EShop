using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Inventory.Outstocks.Dtos
{
    public class GetOutstockListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 商品SKU ID
        /// </summary>
        public Guid? ProductSkuId { get; set; }

        /// <summary>
        /// 商品SPU ID
        /// </summary>
        public Guid? ProductId { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid? StoreId { get; set; }

        /// <summary>
        /// 筛选
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 创建时间开始
        /// </summary>
        public DateTime? MinCreationTime { get; set; }

        /// <summary>
        /// 创建时间结束
        /// </summary>
        public DateTime? MaxCreationTime { get; set; }

        /// <summary>
        /// 出库时间开始
        /// </summary>
        public DateTime? MinOutstockTime { get; set; }

        /// <summary>
        /// 出库时间结束
        /// </summary>
        public DateTime? MaxOutstockTime { get; set; }

        /// <summary>
        /// 出库人
        /// </summary>
        public string OperatorName { get; set; }
    }
}