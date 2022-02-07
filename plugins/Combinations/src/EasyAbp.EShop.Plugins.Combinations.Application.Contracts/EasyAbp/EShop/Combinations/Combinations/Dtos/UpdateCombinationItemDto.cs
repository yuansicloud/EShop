using System;
using System.ComponentModel;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos
{
    [Serializable]
    public class UpdateCombinationItemDto
    {
        [DisplayName("CombinationItemQuantity")]
        public int Quantity { get; set; }

        [DisplayName("CombinationItemMediaResources")]
        public string MediaResources { get; set; }

        [DisplayName("CombinationItemProductUniqueName")]
        public string ProductUniqueName { get; set; }

        [DisplayName("CombinationItemProductDisplayName")]
        public string ProductDisplayName { get; set; }

        [DisplayName("CombinationItemSkuName")]
        public string SkuName { get; set; }

        [DisplayName("CombinationItemSkuDescription")]
        public string SkuDescription { get; set; }

        [DisplayName("CombinationItemCurrency")]
        public string Currency { get; set; }

        [DisplayName("CombinationItemUnitPrice")]
        public decimal UnitPrice { get; set; }

        [DisplayName("CombinationItemTotalPrice")]
        public decimal TotalPrice { get; set; }

        [DisplayName("CombinationItemTotalDiscount")]
        public decimal TotalDiscount { get; set; }

        [DisplayName("CombinationItemIsFixedPrice")]
        public bool IsFixedPrice { get; set; }

        [DisplayName("CombinationItemUnit")]
        public string Unit { get; set; }

    }
}