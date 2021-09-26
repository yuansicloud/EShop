using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class GetProductSkuListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid? StoreId { get; set; }

        /// <summary>
        /// 是否显示隐藏商品（需要权限）
        /// </summary>
        public bool ShowHidden { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public bool? IsPublished { get; set; }

        /// <summary>
        /// 筛选
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 最小价格
        /// </summary>
        public decimal? MinimumPrice { get; set; }

        /// <summary>
        /// 最大价格
        /// </summary>
        public decimal? MaximumPrice { get; set; }

    }
}