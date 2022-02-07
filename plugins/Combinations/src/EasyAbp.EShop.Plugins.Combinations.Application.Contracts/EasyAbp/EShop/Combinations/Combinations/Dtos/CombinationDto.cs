using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos
{
    [Serializable]
    public class CombinationDto : FullAuditedEntityDto<Guid>
    {
        public Guid CombinationDetailId { get; set; }

        public string UniqueName { get; set; }

        public string DisplayName { get; set; }

        public string MediaResources { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsPublished { get; set; }

        public Guid StoreId { get; set; }

        public List<CombinationItemDto> CombinationItems { get; set; }

        public decimal Price
        {
            get
            {
                return CombinationItems.Sum(x => x.Quantity * x.UnitPrice - x.TotalDiscount);
            }
        }

        public decimal OriginalPrice
        {
            get
            {
                return CombinationItems.Sum(x => x.Quantity * x.UnitPrice);
            }
        }

    }
}