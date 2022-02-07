using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Combinations.CombinationDetails
{
    public static class CombinationDetailEfCoreQueryableExtensions
    {
        public static IQueryable<CombinationDetail> IncludeDetails(this IQueryable<CombinationDetail> queryable, bool include = true)
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