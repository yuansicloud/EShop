namespace EasyAbp.EShop.Products.Categories.Dtos
{
    using System;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// Defines the <see cref="GetCategoryListDto" />.
    /// </summary>
    [Serializable]
    public class GetCategoryListDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 筛选(名称及备注)
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int? Level { get; set; }

        /// <summary>
        /// 显示隐藏
        /// </summary>
        public bool ShowHidden { get; set; }
    }
}
