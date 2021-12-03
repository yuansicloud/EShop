using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class InventoryChangeFailedException : BusinessException
    {
        public InventoryChangeFailedException(Guid productId, Guid productSkuId) : base(
            message:
            $"Inventory of product {productId} (SKU: {productSkuId}) cannot be changed")
        {
        }
    }
}