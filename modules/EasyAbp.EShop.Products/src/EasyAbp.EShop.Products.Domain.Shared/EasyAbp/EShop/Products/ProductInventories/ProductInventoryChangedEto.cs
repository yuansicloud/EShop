using System;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductInventories
{
    [Serializable]
    public class ProductInventoryChangedEto : IMultiTenant, IHasExtraProperties
    {
        public Guid? TenantId { get; set; }

        public Guid StoreId { get; set; }
        
        public Guid ProductId { get; set; }
        
        public Guid ProductSkuId { get; set; }
        
        public int OriginalInventory { get; set; }
        
        public int NewInventory { get; set; }
        
        public long Sold { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public ProductInventoryChangedEto(
            Guid? tenantId,
            Guid storeId,
            Guid productId,
            Guid productSkuId,
            int originalInventory,
            int newInventory,
            long sold,
            ExtraPropertyDictionary extraProperties = null)
        {
            TenantId = tenantId;
            StoreId = storeId;
            ProductId = productId;
            ProductSkuId = productSkuId;
            OriginalInventory = originalInventory;
            NewInventory = newInventory;
            Sold = sold;
            ExtraProperties = extraProperties;
        }
    }
}