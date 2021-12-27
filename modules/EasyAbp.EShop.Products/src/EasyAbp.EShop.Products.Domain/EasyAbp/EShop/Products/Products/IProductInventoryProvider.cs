using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductInventoryProvider
    {
        Task<InventoryDataModel> GetInventoryDataAsync(Product product, ProductSku productSku);
        
        Task<Dictionary<Guid, InventoryDataModel>> GetInventoryDataDictionaryAsync(Product product);

        Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, int quantity, bool decreaseSold, ExtraPropertyDictionary extraProperties = null);
        
        Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, int quantity, bool increaseSold, ExtraPropertyDictionary extraProperties = null);
    }
}