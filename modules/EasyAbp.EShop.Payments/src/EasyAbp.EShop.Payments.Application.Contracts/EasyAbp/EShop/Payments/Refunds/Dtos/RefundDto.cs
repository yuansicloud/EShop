using EasyAbp.PaymentService.Refunds;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    [Serializable]
    public class RefundDto : ExtensibleFullAuditedEntityDto<Guid>, IRefund
    {
        #region Base properties

        public Guid PaymentId { get; set; }

        public string RefundPaymentMethod { get; set; }

        public string ExternalTradingCode { get; set; }

        public string Currency { get; set; }

        public decimal RefundAmount { get; set; }

        public string DisplayReason { get; set; }

        public string CustomerRemark { get; set; }

        public string StaffRemark { get; set; }

        public DateTime? CompletedTime { get; set; }

        public DateTime? CanceledTime { get; set; }

        #endregion Base properties

        public List<RefundItemDto> RefundItems { get; set; }

        public bool IsCompleted
        {
            get { return CompletedTime.HasValue; }
        }

        public bool IsCancelled
        {
            get { return CanceledTime.HasValue; }
        }

        public int RefundItemsCount
        {
            get
            {
                return RefundItems.IsNullOrEmpty() ? 0 : RefundItems.Count;
            }
        }
    }
}