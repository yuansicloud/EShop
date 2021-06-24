using System;

namespace EasyAbp.EShop.Products.Products
{
    [Serializable]
    public class ProductInventoryReductionAfterOrderConsumedResultEto
    {
        public Guid? TenantId { get; set; }

        public Guid OrderId { get; set; }
        
        public bool IsSuccess { get; set; }
    }
}