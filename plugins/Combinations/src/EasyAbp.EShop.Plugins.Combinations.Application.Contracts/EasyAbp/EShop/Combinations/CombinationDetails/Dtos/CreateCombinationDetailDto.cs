using System;
using System.ComponentModel;
namespace EasyAbp.EShop.Plugins.Combinations.CombinationDetails.Dtos
{
    [Serializable]
    public class CreateCombinationDetailDto
    {
        [DisplayName("CombinationDetailStoreId")]
        public Guid? StoreId { get; set; }

        [DisplayName("CombinationDetailDescription")]
        public string Description { get; set; }
    }
}