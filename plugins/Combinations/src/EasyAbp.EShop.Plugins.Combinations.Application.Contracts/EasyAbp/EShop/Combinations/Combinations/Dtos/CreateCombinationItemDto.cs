using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos
{
    [Serializable]
    public class CreateCombinationItemDto : ExtensibleObject
    {
        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }

        public int Quantity { get; set; }

        [DisplayName("CombinationItemUnitPrice")]
        public decimal? UnitPrice { get; set; }

        [DisplayName("CombinationItemTotalDiscount")]
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
        }

    }
}