using System;
using System.ComponentModel;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos
{
    [Serializable]
    public class UpdateCombinationDto
    {
        [DisplayName("CombinationCombinationDetailId")]
        public Guid CombinationDetailId { get; set; }

        [DisplayName("CombinationUniqueName")]
        public string UniqueName { get; set; }

        [DisplayName("CombinationDisplayName")]
        public string DisplayName { get; set; }

        [DisplayName("CombinationMediaResources")]
        public string MediaResources { get; set; }

        [DisplayName("CombinationDisplayOrder")]
        public int DisplayOrder { get; set; }

        [DisplayName("CombinationIsPublished")]
        public bool IsPublished { get; set; }

    }
}