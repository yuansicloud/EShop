using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<ProductWithExtraData> GetWithExtraDataAsync(Guid id, Guid storeId);
        
        Task<ProductWithExtraData> GetByUniqueNameWithExtraDataAsync(string uniqueName, Guid storeId);
        
        IQueryable<ProductWithExtraData> GetQueryable(Guid storeId, Guid? categoryId = null);

        IQueryable<ProductWithExtraData> WithDetails(Guid storeId, Guid? categoryId = null);
    }
}