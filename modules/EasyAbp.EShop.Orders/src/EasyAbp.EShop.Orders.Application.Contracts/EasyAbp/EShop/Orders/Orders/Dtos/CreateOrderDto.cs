using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Orders.Orders.Dtos
{
    [Serializable]
    public class CreateOrderDto : ExtensibleObject, IMultiStore
    {
        [DisplayName("OrderStoreId")]
        public Guid StoreId { get; set; }

        [DisplayName("OrderCustomerRemark")]
        public string CustomerRemark { get; set; }

        public string StaffRemark { get; set; }

        public Guid? CustomerUserId { get; set; }

        public Guid? StaffUserId { get; set; }

        public Guid? GraveId { get; set; }

        public Guid? RegionId { get; set; }

        public Guid? OccupantId { get; set; }

        public string ResourceType { get; set; }

        public Guid? ResourceId { get; set; }

        public string OrderType { get; set; }

        [DisplayName("OrderLine")]
        public List<CreateOrderLineDto> OrderLines { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            base.Validate(validationContext);
            
            if (OrderLines.Count == 0)
            {
                yield return new ValidationResult(
                    "OrderLines should not be empty.",
                    new[] { "OrderLines" }
                );
            }
            
            if (OrderLines.Any(orderLine => orderLine.Quantity <= 0))
            {
                yield return new ValidationResult(
                    "Quantity should be greater than 0.",
                    new[] { "OrderLines" }
                );
            }
        }
    }
}