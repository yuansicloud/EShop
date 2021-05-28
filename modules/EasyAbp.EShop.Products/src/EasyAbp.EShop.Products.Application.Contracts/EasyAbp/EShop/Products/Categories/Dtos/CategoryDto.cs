namespace EasyAbp.EShop.Products.Categories.Dtos
{
    using EasyAbp.EShop.Products.Helpers;
    using JetBrains.Annotations;
    using System;
    using System.Collections.Generic;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 商品类别模型
    /// </summary>
    [Serializable]
    public class CategoryDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string UniqueName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 类别代码
        /// </summary>
        [NotNull]
        public string Code { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<CategoryDto> Children { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 媒体文件
        /// </summary>
        public string MediaResources { get; set; }

        /// <summary>
        /// 中文级别显示
        /// </summary>
        public string LevelToChinese
        {
            get
            {
                return Level.ToCNNumberString();
            }
        }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden { get; set; }
    }
}
