using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EasyAbp.EShop.Inventory.Instocks.Dtos
{
    public class InstockMultiCreateViewModel
    {
        [DisplayName("InstockWarehouseId")]
        public Guid WarehouseId { get; set; }

        [DisplayName("InstockStoreId")]
        public Guid StoreId { get; set; }

        [DisplayName("InstockDescription")]
        public string Description { get; set; }

        [DisplayName("InstockInstockTime")]
        public DateTime InstockTime { get; set; }

        public List<InstockMultiCreateEntityViewModel> Instocks { get; set; }
    }
}