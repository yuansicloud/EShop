using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSku : FullAuditedEntity<Guid>, IProductSku
    {
        [NotNull]
        public virtual string SerializedAttributeOptionIds { get; protected set; }
        
        [CanBeNull]
        public virtual string Name { get; protected set; }
        
        [NotNull]
        public virtual string Currency { get; protected set; }
        
        public virtual decimal? OriginalPrice { get; protected set; }
        
        public virtual decimal Price { get; protected set; }

        public virtual int OrderMinQuantity { get; protected set; }
        
        public virtual int OrderMaxQuantity { get; protected set; }
        
        [CanBeNull]
        public virtual string MediaResources { get; protected set; }

        public virtual Guid? ProductDetailId { get; protected set; }
        
        public virtual ExtraPropertyDictionary ExtraProperties { get; protected set; }

        public virtual bool IsFixedPrice { get; protected set; }

        public virtual int Threshold { get; protected set; }

        public virtual Guid ProductId { get; protected set; }

        protected ProductSku()
        {
            ExtraProperties = new ExtraPropertyDictionary();
            this.SetDefaultsForExtraProperties();
        }

        public ProductSku(
            Guid id,
            [NotNull] string serializedAttributeOptionIds,
            [CanBeNull] string name,
            [NotNull] string currency,
            decimal? originalPrice,
            decimal price,
            int orderMinQuantity,
            int orderMaxQuantity,
            int threshold,
            Guid productId,
            bool isFixedPrice,
            [CanBeNull] string mediaResources,
            Guid? productDetailId) : base(id)
        {
            SerializedAttributeOptionIds = serializedAttributeOptionIds;
            Name = name?.Trim();
            Currency = currency;
            OriginalPrice = originalPrice;
            Price = price;
            OrderMinQuantity = orderMinQuantity;
            OrderMaxQuantity = orderMaxQuantity;
            MediaResources = mediaResources;
            ProductDetailId = productDetailId;
            IsFixedPrice = isFixedPrice;
            Threshold = threshold;
            ProductId = productId;

            ExtraProperties = new ExtraPropertyDictionary();
            this.SetDefaultsForExtraProperties();
        }

        internal void TrimCode()
        {
            Name = Name?.Trim();
        }

        public void SetSerializedAttributeOptionIds(string serializedAttributeOptionIds)
        {
            SerializedAttributeOptionIds = serializedAttributeOptionIds;
        }

        public void SetThreshold(int threshold)
        {
            Threshold = threshold;
        }
    }
}