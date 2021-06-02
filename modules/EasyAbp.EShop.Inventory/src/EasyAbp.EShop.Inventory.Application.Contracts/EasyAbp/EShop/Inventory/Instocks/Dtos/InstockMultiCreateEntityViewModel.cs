using System;
using System.ComponentModel;

namespace EasyAbp.EShop.Inventory.Instocks.Dtos
{
    public class InstockMultiCreateEntityViewModel
    {
        [DisplayName("InstockProductSkuId")]
        public Guid ProductSkuId { get; set; }

        [DisplayName("InstockUnitPrice")]
        public decimal UnitPrice { get; set; }

        [DisplayName("InstockUnits")]
        public int Units { get; set; }

        [DisplayName("InstockOperatorName")]
        public string OperatorName { get; set; }

        public Guid ProductId { get; set; }
    }
}