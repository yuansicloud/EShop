using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public interface IProductData
    {
        string MediaResources { get; }

        string ProductUniqueName { get; }

        string ProductDisplayName { get; }

        string SkuName { get; }

        string SkuDescription { get; }

        string Currency { get; }

        decimal UnitPrice { get; }

        decimal TotalPrice { get; }

        decimal TotalDiscount { get; }

        bool IsFixedPrice { get; }

        string Unit { get; }

        Guid ProductId { get; }

        Guid ProductSkuId { get; }
    }
}
