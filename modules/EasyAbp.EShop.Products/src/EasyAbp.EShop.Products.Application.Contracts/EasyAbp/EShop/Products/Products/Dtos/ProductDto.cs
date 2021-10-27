namespace EasyAbp.EShop.Products.Products.Dtos
{
    using EasyAbp.EShop.Products.ProductCategories.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// ��Ʒģ��
    /// </summary>
    [Serializable]
    public class ProductDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// ����ID
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string ProductGroupName { get; set; }

        /// <summary>
        /// ��Ʒ����ʾ����
        /// </summary>
        public string ProductGroupDisplayName { get; set; }

        /// <summary>
        /// ����ID
        /// </summary>
        public Guid ProductDetailId { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string UniqueName { get; set; }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public InventoryStrategy InventoryStrategy { get; set; }

        /// <summary>
        /// ý���ļ�
        /// </summary>
        public string MediaResources { get; set; }

        /// <summary>
        /// ��ʾ˳��
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// �Ƿ��ѷ���
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// �Ƿ��Ǿ�̬
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsHidden { get; set; }
        
        public TimeSpan? PaymentExpireIn { get; set; }

        public long Sold { get; set; }

        /// <summary>
        /// ��С�۸�
        /// </summary>
        public decimal? MinimumPrice { get; set; }

        /// <summary>
        /// ���۸�
        /// </summary>
        public decimal? MaximumPrice { get; set; }

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public ICollection<ProductAttributeDto> ProductAttributes { get; set; }

        /// <summary>
        /// ��ƷSKU
        /// </summary>
        public ICollection<ProductSkuDto> ProductSkus { get; set; }

        /// <summary>
        /// ��Ʒ���
        /// </summary>
        public ICollection<ProductCategoryDto> ProductCategories { get; set; }

        /// <summary>
        /// ��Ʒ�������
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

        public TimeSpan? GetSkuPaymentExpireIn(Guid skuId)
        {
            var sku = GetSkuById(skuId);

            return sku.PaymentExpireIn ?? PaymentExpireIn;
        }
    }
}