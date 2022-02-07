using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos
{
    [Serializable]
    public class CombinationItemDto : FullAuditedEntityDto<Guid>
    {
        public int Quantity { get; set; }

        public string MediaResources { get; set; }

        public string ProductUniqueName { get; set; }

        public string ProductDisplayName { get; set; }

        public string SkuName { get; set; }

        public string SkuDescription { get; set; }

        public string Currency { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscount { get; set; }

        public bool IsFixedPrice { get; set; }

        public string Unit { get; set; }

    }
}