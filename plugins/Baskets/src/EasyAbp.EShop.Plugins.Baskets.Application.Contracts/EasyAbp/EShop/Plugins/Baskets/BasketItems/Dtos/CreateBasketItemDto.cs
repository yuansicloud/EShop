using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos
{
    [Serializable]
    public class CreateBasketItemDto : ExtensibleObject
    {
        public string BasketName { get; set; } = BasketsConsts.DefaultBasketName;
        
        /// <summary>
        /// Specify the basket item owner user ID. Use current user ID if this property is null.
        /// </summary>
        public Guid? IdentifierId { get; set; }
        
        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }
        
        public int Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal TotalDiscount { get; set; } = 0;

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            base.Validate(validationContext);
            
            if (Quantity <= 0)
            {
                yield return new ValidationResult(
                    "Quantity should be greater than 0.",
                    new[] { "Quantity" }
                );
            }

            if (UnitPrice.HasValue && UnitPrice.Value < 0)
            {
                yield return new ValidationResult(
                    "UnitPrice should be greater than 0.",
                    new[] { "UnitPrice" }
                );
            }

            if (TotalDiscount < 0)
            {
                yield return new ValidationResult(
                    "TotalDiscount should be greater than 0.",
                    new[] { "TotalDiscount" }
                );
            }

            if (BasketName.IsNullOrWhiteSpace())
            {
                yield return new ValidationResult(
                    "BasketName should not be empty.",
                    new[] { "BasketName" }
                );
            }
        }
    }
}