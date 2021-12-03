﻿using EasyAbp.EShop.Inventory.Inventories;
using EasyAbp.EShop.Stores.Stores;
using System;
using System.ComponentModel;
using Volo.Abp.ObjectExtending;
using Address = EasyAbp.EShop.Inventory.Inventories.Address;

namespace EasyAbp.EShop.Inventory.Warehouses.Dtos
{
    [Serializable]
    public class WarehouseUpdateDto : ExtensibleObject, IMultiStore
    {
        [DisplayName("WarehouseName")]
        public string Name { get; set; }

        [DisplayName("WarehouseAddress")]
        public Address Address { get; set; }

        [DisplayName("WarehouseDescription")]
        public string Description { get; set; }

        [DisplayName("WarehouseContactName")]
        public string ContactName { get; set; }

        [DisplayName("WarehouseContactPhoneNumber")]
        public string ContactPhoneNumber { get; set; }

        [DisplayName("WarehouseStoreId")]
        public Guid StoreId { get; set; }

    }
}