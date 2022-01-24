using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos
{
    public class CreateOrderFromBasketInput
    {
        public string BasketName { get; set; } = BasketsConsts.DefaultBasketName;

        /// <summary>
        /// Specify the basket item owner user ID. Use current user ID if this property is null.
        /// </summary>
        public Guid? IdentifierId { get; set; }

        //[DisplayName("OrderStoreId")]
        //public Guid StoreId { get; set; }

        [DisplayName("OrderCustomerRemark")]
        public string CustomerRemark { get; set; }

        public string StaffRemark { get; set; }

        public Guid? CustomerUserId { get; set; }

        public Guid? StaffUserId { get; set; }

        public Guid? GraveId { get; set; }

        public Guid? OccupantId { get; set; }

        public string OrderType { get; set; }
    }
}
