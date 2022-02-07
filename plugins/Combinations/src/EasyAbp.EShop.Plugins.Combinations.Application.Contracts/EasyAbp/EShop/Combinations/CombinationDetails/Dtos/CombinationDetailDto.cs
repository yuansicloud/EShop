using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Combinations.CombinationDetails.Dtos
{
    [Serializable]
    public class CombinationDetailDto : FullAuditedEntityDto<Guid>
    {
        public Guid? StoreId { get; set; }

        public string Description { get; set; }
    }
}