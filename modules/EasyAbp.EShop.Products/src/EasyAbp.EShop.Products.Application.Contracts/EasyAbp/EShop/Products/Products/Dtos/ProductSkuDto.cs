namespace EasyAbp.EShop.Products.Products.Dtos
{
    using System;
    using System.Collections.Generic;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 商品SKU模型
    /// </summary>
    [Serializable]
    public class ProductSkuDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 商品SKU属性值IDs
        /// </summary>
        public List<Guid> AttributeOptionIds { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public decimal? OriginalPrice { get; set; }

        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 折扣价格
        /// </summary>
        public decimal DiscountedPrice { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Inventory { get; set; }

        /// <summary>
        /// 已售数量
        /// </summary>
        public long Sold { get; set; }

        /// <summary>
        /// 最小一次下单数
        /// </summary>
        public int OrderMinQuantity { get; set; }

        /// <summary>
        /// 最大一次下单数
        /// </summary>
        public int OrderMaxQuantity { get; set; }

        /// <summary>
        /// 媒体文件
        /// </summary>
        public string MediaResources { get; set; }

        /// <summary>
        /// 详情ID
        /// </summary>
        public Guid? ProductDetailId { get; set; }

        /// <summary>
        /// 可否调价（true 为不可调价）
        /// </summary>
        public bool IsFixedPrice { get; set; }

        /// <summary>
        /// 预警值 
        /// </summary>
        public int Threshold { get; set; }
    }
}
