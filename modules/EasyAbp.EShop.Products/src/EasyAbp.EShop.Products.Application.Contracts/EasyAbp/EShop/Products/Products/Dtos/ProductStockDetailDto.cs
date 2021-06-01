using EasyAbp.EShop.Inventory.StockDetails;
using System;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class ProductStockDetailDto : IProductStockDetail
    {
        public int LockedQuantity { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public int InstockQuantity { get; set; }

        public int OutStockQuantity { get; set; }

        public int TermStartQuantity { get; set; }

        public int TermEndQuantity { get; set; }
    }
}