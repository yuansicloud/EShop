using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class GetProductListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        [Required]
        public Guid StoreId { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// 是否包括子类别的商品
        /// </summary>
        public bool ShowRecursive { get; set; }

        ///// <summary>
        ///// 是否显示真实详细库存
        ///// </summary>
        //public bool ShowStock { get; set; } = false;

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

        ///// <summary>
        ///// 库存记录开始时间
        ///// </summary>
        //public DateTime? TermStartTime { get; set; }

        ///// <summary>
        ///// 库存记录结束时间
        ///// </summary>
        //public DateTime? TermEndTime { get; set; }
    }
}