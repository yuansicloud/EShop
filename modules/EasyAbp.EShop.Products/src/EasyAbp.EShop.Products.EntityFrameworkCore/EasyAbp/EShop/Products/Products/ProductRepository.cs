using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using EasyAbp.EShop.Products.ProductInventories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductRepository : EfCoreRepository<IProductsDbContext, Product, Guid>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<IProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override IQueryable<Product> WithDetails()
        {
            return base.WithDetails()
                .Include(x => x.ProductAttributes).ThenInclude(x => x.ProductAttributeOptions)
                .Include(x => x.ProductSkus);
        }

        public async Task<ProductWithExtraData> GetWithExtraDataAsync(Guid id, Guid storeId)
        {
            var queryable = WithDetails().Where(x => x.Id == id);

            return await GetProductWithExtraDataQueryable(queryable, storeId).SingleAsync();
        }

        public async Task<ProductWithExtraData> GetByUniqueNameWithExtraDataAsync(string uniqueName, Guid storeId)
        {
            var queryable = WithDetails().Where(x => x.UniqueName == uniqueName);

            return await GetProductWithExtraDataQueryable(queryable, storeId).SingleAsync();
        }

        public IQueryable<ProductWithExtraData> GetQueryable(Guid storeId, Guid? categoryId = null)
        {
            var queryable = GetStoreQueryable(storeId);

            if (categoryId.HasValue)
            {
                queryable = JoinProductCategories(queryable, categoryId.Value);
            }

            return GetProductWithExtraDataQueryable(queryable, storeId);
        }

        protected IQueryable<ProductWithExtraData> GetProductWithExtraDataQueryable(IQueryable<Product> queryable,
            Guid storeId)
        {
            var productInventoryRepository = ServiceProvider.GetRequiredService<IProductInventoryRepository>();

            return queryable.Select(x => new
                ProductWithExtraData
                {
                    Product = x,
                    Inventory = productInventoryRepository.Where(y => y.ProductId == x.Id && y.StoreId == storeId)
                        .Sum(y => y.Inventory),
                    Sold = productInventoryRepository.Where(y => y.ProductId == x.Id && y.StoreId == storeId)
                        .Sum(y => y.Sold),
                });
        }

        public IQueryable<ProductWithExtraData> WithDetails(Guid storeId, Guid? categoryId = null)
        {
            var queryable = WithStoreDetails(storeId);

            if (categoryId.HasValue)
            {
                queryable = JoinProductCategories(queryable, categoryId.Value);
            }

            return GetProductWithExtraDataQueryable(queryable, storeId);
        }

        protected virtual IQueryable<Product> GetStoreQueryable(Guid storeId)
        {
            return JoinProductStores(GetQueryable(), storeId);
        }

        protected virtual IQueryable<Product> WithStoreDetails(Guid storeId)
        {
            return JoinProductStores(WithDetails(), storeId);
        }

        protected virtual IQueryable<Product> JoinProductStores(IQueryable<Product> queryable, Guid storeId)
        {
            return queryable.Join(
                DbContext.ProductStores.Where(productStore => productStore.StoreId == storeId),
                product => product.Id,
                productStore => productStore.ProductId,
                (product, productStore) => product
            );
        }

        protected virtual IQueryable<Product> JoinProductCategories(IQueryable<Product> queryable, Guid categoryId)
        {
            return queryable.Join(
                DbContext.ProductCategories.Where(productCategory => productCategory.CategoryId == categoryId),
                product => product.Id,
                productCategory => productCategory.ProductId,
                (product, productCategory) => product
            );
        }
    }
}