using System;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductWithExtraData
    {
        public Product Product { get; set; }
        
        public long Inventory { get; set; }
        
        public long Sold { get; set; }
    }
}