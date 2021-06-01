using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<IQueryable<Product>> GetQueryableAsync(Guid categoryId, bool showRecursive);

        Task<IQueryable<Product>> WithDetailsAsync(Guid categoryId, bool showRecursive);

        Task CheckUniqueNameAsync(Product entity, CancellationToken cancellationToken = new CancellationToken());
    }
}