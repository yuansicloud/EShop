using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    [Serializable]
    public class RefundItemOrderLineDto : EntityDto<Guid>
    {
        public Guid OrderLineId { get; set; }
        
        public int RefundedQuantity { get; set; }
        
        public decimal RefundAmount { get; set; }
    }
}