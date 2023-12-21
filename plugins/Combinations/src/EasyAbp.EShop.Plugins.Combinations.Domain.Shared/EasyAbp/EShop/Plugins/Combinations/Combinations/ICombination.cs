using EasyAbp.EShop.Stores.Stores;
using System;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public interface ICombination : IHasExtraProperties
    {

        Guid CombinationDetailId { get; }

        string UniqueName { get; }

        string DisplayName { get; }

        string MediaResources { get; }

        int DisplayOrder { get; }

        bool IsPublished { get; }

    }
}
