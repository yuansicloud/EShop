using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public static class CombinationItemEfCoreQueryableExtensions
    {
        public static IQueryable<CombinationItem> IncludeDetails(this IQueryable<CombinationItem> queryable, bool include = true)
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