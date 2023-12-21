using System;
using System.ComponentModel;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos
{
    [Serializable]
    public class UpdateCombinationItemDto
    {
        [DisplayName("CombinationItemQuantity")]
        public int Quantity { get; set; }

        [DisplayName("CombinationItemUnitPrice")]
        public decimal? UnitPrice { get; set; }

        [DisplayName("CombinationItemTotalDiscount")]
        public decimal TotalDiscount { get; set; }

    }
}