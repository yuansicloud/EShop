namespace EasyAbp.EShop.Products.Products.Dtos
{
    using EasyAbp.EShop.Inventory.Aggregates.Dtos;
    using EasyAbp.EShop.Products.ProductCategories.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 商品模型
    /// </summary>
    [Serializable]
    public class ProductDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// 商品组名
        /// </summary>
        public string ProductGroupName { get; set; }

        /// <summary>
        /// 商品组显示名称
        /// </summary>
        public string ProductGroupDisplayName { get; set; }

        /// <summary>
        /// 详情ID
        /// </summary>
        public Guid ProductDetailId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string UniqueName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 库存策略
        /// </summary>
        public InventoryStrategy InventoryStrategy { get; set; }

        /// <summary>
        /// 媒体文件
        /// </summary>
        public string MediaResources { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否已发布
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// 是否是静态
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// 已售数量
        /// </summary>
        public long Sold { get; set; }

        /// <summary>
        /// 最小价格
        /// </summary>
        public decimal? MinimumPrice { get; set; }

        /// <summary>
        /// 最大价格
        /// </summary>
        public decimal? MaximumPrice { get; set; }

        /// <summary>
        /// 商品属性
        /// </summary>
        public ICollection<ProductAttributeDto> ProductAttributes { get; set; }

        /// <summary>
        /// 商品SKU
        /// </summary>
        public ICollection<ProductSkuDto> ProductSkus { get; set; }

        /// <summary>
        /// 商品类别
        /// </summary>
        public ICollection<ProductCategoryDto> ProductCategories { get; set; }

        /// <summary>
        /// 商品库存详情
        /// </summary>
        public ProductStockDetailDto ProductStockDetail { get; set; }

        /// The GetSkuById.
        /// </summary>
        /// <param name="skuId">The skuId<see cref="Guid"/>.</param>
        /// <returns>The <see cref="ProductSkuDto"/>.</returns>
        public ProductSkuDto GetSkuById(Guid skuId)
        {
            return ProductSkus.Single(x => x.Id == skuId);
        }

        /// <summary>
        /// The FindSkuById.
        /// </summary>
        /// <param name="skuId">The skuId<see cref="Guid"/>.</param>
        /// <returns>The <see cref="ProductSkuDto"/>.</returns>
        public ProductSkuDto FindSkuById(Guid skuId)
        {
            return ProductSkus.FirstOrDefault(x => x.Id == skuId);
        }
    }
}
