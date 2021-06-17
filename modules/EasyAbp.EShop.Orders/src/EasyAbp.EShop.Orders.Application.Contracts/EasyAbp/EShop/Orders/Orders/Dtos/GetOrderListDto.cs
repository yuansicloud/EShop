using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Orders.Orders.Dtos
{
    [Serializable]
    public class GetOrderListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public Guid? StoreId { get; set; }
        
        public Guid? CustomerUserId { get; set; }

        public Guid? StaffUserId { get; set; }

        public Guid? OccupantId { get; set; }

        public Guid? GraveId { get; set; }

        public int? MaxActualTotalPrice { get; set; }

        public int? MinActualTotalPrice { get; set; }

        public DateTime? MaxCreationDate { get; set; }

        public DateTime? MinCreationDate { get; set; }

        public OrderStatus? OrderStatus { get; set; }

        [Range(1, 10000)]
        public override int MaxResultCount { get; set; } = DefaultMaxResultCount;
    }
}