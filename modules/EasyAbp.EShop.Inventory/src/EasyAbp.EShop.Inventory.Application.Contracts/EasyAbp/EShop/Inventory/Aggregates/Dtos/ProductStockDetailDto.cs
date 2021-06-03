using EasyAbp.EShop.Inventory.StockDetails;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyAbp.EShop.Inventory.Aggregates.Dtos
{
    public class ProductStockDetailDto : IProductStockDetail
    {
        public int LockedQuantity
        {
            get
            {
                return ProductSkuStockDetails.Sum(s => s.LockedQuantity);
            }
        }

        public Guid ProductId { get; set; }

        public int Quantity
        {
            get
            {
                return ProductSkuStockDetails.Sum(s => s.Quantity);
            }
        }

        public int InstockQuantity
        {
            get
            {
                return ProductSkuStockDetails.Sum(s => s.InstockQuantity);
            }
        }

        public int OutStockQuantity
        {
            get
            {
                return ProductSkuStockDetails.Sum(s => s.OutStockQuantity);
            }
        }

        public int TermStartQuantity
        {
            get
            {
                return ProductSkuStockDetails.Sum(s => s.TermStartQuantity ?? 0);
            }
        }

        public int TermEndQuantity
        {
            get
            {
                return ProductSkuStockDetails.Sum(s => s.TermEndQuantity ?? 0);
            }
        }

        public List<ProductSkuStockDetailDto> ProductSkuStockDetails { get; set; } = new();
    }
}