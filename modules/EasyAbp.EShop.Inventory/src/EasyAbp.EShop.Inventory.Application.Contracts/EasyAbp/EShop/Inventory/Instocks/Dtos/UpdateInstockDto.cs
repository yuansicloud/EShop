using System;

namespace EasyAbp.EShop.Inventory.Instocks.Dtos
{
    public class UpdateInstockDto
    {
        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public DateTime InstockTime { get; set; }

        public string OperatorName { get; set; }

        public InstockType InstockType { get; set; }
    }
}
