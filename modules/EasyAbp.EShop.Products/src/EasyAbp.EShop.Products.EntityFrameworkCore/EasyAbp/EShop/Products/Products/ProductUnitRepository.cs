using EasyAbp.EShop.Products.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductUnitRepository : EfCoreRepository<IProductsDbContext, ProductUnit, Guid>, IProductUnitRepository
    {
        public ProductUnitRepository(IDbContextProvider<IProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<ProductUnit> UpdateAsync(ProductUnit entity, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            await CheckUniqueNameAsync(entity, cancellationToken);

            return await base.UpdateAsync(entity, autoSave, cancellationToken);
        }

        public override async Task<ProductUnit> InsertAsync(ProductUnit entity, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            await CheckUniqueNameAsync(entity, cancellationToken);

            return await base.InsertAsync(entity, autoSave, cancellationToken);
        }

        public virtual async Task CheckUniqueNameAsync(ProductUnit entity, CancellationToken cancellationToken = new CancellationToken())
        {
            if (entity.DisplayName.IsNullOrEmpty())
            {
                return;
            }

            if (await (await GetDbSetAsync()).AnyAsync(
                x => x.DisplayName == entity.DisplayName && x.Id != entity.Id,
                cancellationToken))
            {
                throw new DuplicatedProductUniqueNameException(entity.DisplayName);
            }
        }
    }
}