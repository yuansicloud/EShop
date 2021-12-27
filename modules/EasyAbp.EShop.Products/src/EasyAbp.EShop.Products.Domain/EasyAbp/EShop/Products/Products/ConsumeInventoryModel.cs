using System;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.Products
{
    public class ConsumeInventoryModel : IHasExtraProperties
    {
        public Product Product { get; set; }
        
        public ProductSku ProductSku { get; set; }
        
        public Guid StoreId { get; set; }
        
        public int Quantity { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}