namespace EasyAbp.EShop.Products.Products.Dtos
{
    using EasyAbp.EShop.Stores.Stores;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Volo.Abp.ObjectExtending;

    /// <summary>
    /// 创建产品模型
    /// </summary>
    public class CreateProductViewModel : ExtensibleObject, IMultiStore
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        [DisplayName("ProductStoreId")]
        public Guid StoreId { get; set; }

        /// <summary>
        /// 产品组名称
        /// </summary>
        [DisplayName("ProductProductGroupName")]
        public string ProductGroupName { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        [DisplayName("ProductDetailDescription")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the CategoryIds.
        /// </summary>
        [DisplayName("ProductCategory")]
        public ICollection<Guid> CategoryIds { get; set; }

        /// <summary>
        /// Gets or sets the UniqueName.
        /// </summary>
        [DisplayName("ProductUniqueName")]
        public string UniqueName { get; set; }

        /// <summary>
        /// Gets or sets the DisplayName.
        /// </summary>
        [Required]
        [DisplayName("ProductDisplayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the ProductAttributes.
        /// </summary>
        public ICollection<CreateUpdateProductAttributeDto> ProductAttributes { get; set; }

        /// <summary>
        /// Gets or sets the ProductSkus.
        /// </summary>
        public ICollection<CreateProductSkuViewModel> ProductSkus { get; set; }

        /// <summary>
        /// Gets or sets the InventoryStrategy.
        /// </summary>
        [DisplayName("ProductInventoryStrategy")]
        public InventoryStrategy InventoryStrategy { get; set; }

        /// <summary>
        /// Gets or sets the DisplayOrder.
        /// </summary>
        [DisplayName("ProductDisplayOrder")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the MediaResources.
        /// </summary>
        [DisplayName("ProductMediaResources")]
        public string MediaResources { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsPublished.
        /// </summary>
        [DisplayName("ProductIsPublished")]
        public bool IsPublished { get; set; }

        /// <summary>
        /// The Validate.
        /// </summary>
        /// <param name="validationContext">The validationContext<see cref="ValidationContext"/>.</param>
        /// <returns>The <see cref="IEnumerable{ValidationResult}"/>.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            base.Validate(validationContext);

            if (ProductAttributes.Select(a => a.DisplayName.Trim()).Distinct().Count() != ProductAttributes.Count)
            {
                yield return new ValidationResult(
                    "DisplayNames of ProductAttributes should be unique!",
                    new[] { "ProductAttributes" }
                );
            }

            var optionNameList = ProductAttributes.SelectMany(a => a.ProductAttributeOptions)
                .Select(o => o.DisplayName.Trim()).ToList();

            if (optionNameList.Distinct().Count() != optionNameList.Count)
            {
                yield return new ValidationResult(
                    "DisplayNames of ProductAttributeOptions should be unique!",
                    new[] { "ProductAttributeOptions" }
                );
            }

            foreach (var sku in ProductSkus)
            {

                foreach (var option in sku.AttributeOptions)
                {
                    var attribute = ProductAttributes
                        .Where(a => a.DisplayName == option.Attribute
                        && a.ProductAttributeOptions.Select(a => a.DisplayName)
                        .Contains(option.AttributeOption))
                        .Count();

                    if (attribute != 1)
                    {
                        yield return new ValidationResult(
                            "DisplayName of Attribute in Sku not match!",
                            new[] { "ProductAttributeOptions" }
                        );
                    }
                }
            }
        }
    }
}
