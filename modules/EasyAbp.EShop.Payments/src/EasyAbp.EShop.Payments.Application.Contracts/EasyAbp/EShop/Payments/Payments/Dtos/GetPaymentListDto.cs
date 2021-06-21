using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Payments.Dtos
{
    [Serializable]
    public class GetPaymentListDto : PagedAndSortedResultRequestDto
    {
        public Guid? UserId { get; set; }

        public bool? IsCompleted { get; set; }

        public bool? IsCancelled { get; set; }

        public DateTime? MinCompletedTime { get; set; }

        public DateTime? MaxCompletedTime { get; set; }

        public DateTime? MinCancelledTime { get; set; }

        public DateTime? MaxCancelledTime { get; set; }

        public string PaymentMethod { get; set; }

        public string PayeeAccount { get; set; }

        public string ExternalTradingCode { get; set; }

        public decimal? MinPaymentAmount { get; set; }

        public decimal? MaxPaymentAmount { get; set; }

        public DateTime? MinCreationTime { get; set; }

        public DateTime? MaxCreationTime { get; set; }
    }
}