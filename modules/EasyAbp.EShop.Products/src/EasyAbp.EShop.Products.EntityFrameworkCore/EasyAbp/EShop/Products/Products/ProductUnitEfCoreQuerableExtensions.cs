using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.Products
{
    public static class ProductUnitEfCoreQueryableExtensions
    {
        public static IQueryable<ProductUnit> IncludeDetails(this IQueryable<ProductUnit> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                // .Include(x => x.xxx) // TODO: AbpHelper generated
                ;
        }
    }
}