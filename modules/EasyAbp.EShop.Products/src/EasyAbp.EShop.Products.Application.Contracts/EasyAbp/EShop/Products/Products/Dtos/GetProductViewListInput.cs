using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class GetProductViewListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid? StoreId { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// 是否包括子类别的商品
        /// </summary>
        public bool ShowRecursive { get; set; }

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

    }
}