using System;
using EasyAbp.EShop.Plugins.Combinations.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public class CombinationItemRepository : EfCoreRepository<ICombinationsDbContext, CombinationItem, Guid>, ICombinationItemRepository
    {
        public CombinationItemRepository(IDbContextProvider<ICombinationsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}