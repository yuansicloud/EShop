using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSkuRepository : EfCoreRepository<IProductsDbContext, ProductSku, Guid>, IProductSkuRepository
    {
        public ProductSkuRepository(IDbContextProvider<IProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        public override async Task<IQueryable<ProductSku>> WithDetailsAsync()
        {
            return (await base.WithDetailsAsync())
                .Include(x => x.Product)
                .ThenInclude(x => x.ProductAttributes)
                .ThenInclude(x => x.ProductAttributeOptions)
                .Include(x => x.Unit);
        }

    }
}