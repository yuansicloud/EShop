using System;

namespace EasyAbp.EShop.Inventory.Outstocks.Dtos
{
    public class UpdateOutstockDto
    {
        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public DateTime OutstockTime { get; set; }

        public string OperatorName { get; set; }

        public OutstockType OutstockType { get; set; }
    }
}
