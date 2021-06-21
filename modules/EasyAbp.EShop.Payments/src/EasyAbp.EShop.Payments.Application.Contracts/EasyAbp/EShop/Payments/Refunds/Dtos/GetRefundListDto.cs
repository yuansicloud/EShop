using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    [Serializable]
    public class GetRefundListDto : PagedAndSortedResultRequestDto
    {
        public Guid? UserId { get; set; }

        public bool? IsCompleted { get; set; }

        public bool? IsCancelled { get; set; }

        public DateTime? MinCompletedTime { get; set; }

        public DateTime? MaxCompletedTime { get; set; }

        public DateTime? MinCancelledTime { get; set; }

        public DateTime? MaxCancelledTime { get; set; }

        public DateTime? MinCreationTime { get; set; }

        public DateTime? MaxCreationTime { get; set; }

        public decimal? MinRefundAmount { get; set; }

        public decimal? MaxRefundAmount { get; set; }

        public string RefundPaymentMethod { get; set; }

        public string ExternalTradingCode { get; set; }

        public string Filter { get; set; }

    }
}