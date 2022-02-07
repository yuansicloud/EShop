using System;
using EasyAbp.EShop.Plugins.Combinations.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Combinations.CombinationDetails
{
    public class CombinationDetailRepository : EfCoreRepository<ICombinationsDbContext, CombinationDetail, Guid>, ICombinationDetailRepository
    {
        public CombinationDetailRepository(IDbContextProvider<ICombinationsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}