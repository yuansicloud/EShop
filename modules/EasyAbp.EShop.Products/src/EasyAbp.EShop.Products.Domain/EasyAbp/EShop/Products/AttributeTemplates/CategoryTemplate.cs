using System;
using System.Collections.Generic;
using EasyAbp.EShop.Products.Categories;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.Products
{
    public class CategoryTemplate : FullAuditedEntity<Guid>, IHasExtraProperties
    {
        [NotNull]
        public virtual string DisplayName { get; protected set; }
        
        [CanBeNull]
        public virtual string Description { get; protected set; }
        
        public virtual Guid CategoryId { get; protected set; }

        public virtual Category Category { get; protected set; }

        public virtual ExtraPropertyDictionary ExtraProperties { get; protected set; }

        public virtual List<ProductAttributeTemplate> ProductAttributes { get; protected set; }

        protected CategoryTemplate()
        {
            ExtraProperties = new ExtraPropertyDictionary();
            this.SetDefaultsForExtraProperties();
        }
        
        public CategoryTemplate(
            Guid id,
            [NotNull] string displayName,
            [CanBeNull] string description,
            Guid categoryId) : base(id)
        {
            DisplayName = displayName;
            Description = description;
            CategoryId = categoryId;

            ProductAttributes = new List<ProductAttributeTemplate>();
            
            ExtraProperties = new ExtraPropertyDictionary();
            this.SetDefaultsForExtraProperties();
        }
    }
}