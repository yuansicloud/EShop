using EasyAbp.EShop.Inventory.StockDetails;
using System;

namespace EasyAbp.EShop.Inventory.Aggregates.Dtos
{
    public class ProductSkuStockDetailDto : IProductSkuStockDetail
    {
        public int LockedQuantity { get; set; }

        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }

        public int Quantity { get; set; }

        public int InstockQuantity { get; set; }

        public int OutStockQuantity { get; set; }

        public int? TermStartQuantity { get; set; }

        public int? TermEndQuantity { get; set; }
    }
}