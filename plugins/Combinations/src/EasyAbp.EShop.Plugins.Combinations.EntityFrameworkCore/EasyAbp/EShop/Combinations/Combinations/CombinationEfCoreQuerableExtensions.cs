using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public static class CombinationEfCoreQueryableExtensions
    {
        public static IQueryable<Combination> IncludeDetails(this IQueryable<Combination> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                 .Include(x => x.CombinationItems) // TODO: AbpHelper generated
                ;
        }
    }
}