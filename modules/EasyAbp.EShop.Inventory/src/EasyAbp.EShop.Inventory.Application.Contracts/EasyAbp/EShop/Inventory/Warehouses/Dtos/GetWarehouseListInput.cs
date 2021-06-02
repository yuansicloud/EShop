using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EShop.Inventory.Warehouses.Dtos
{
    public class GetWarehouseListInput
    {
        public Guid? StoreId { get; set; }

        public string Filter { get; set; }
    }
}
