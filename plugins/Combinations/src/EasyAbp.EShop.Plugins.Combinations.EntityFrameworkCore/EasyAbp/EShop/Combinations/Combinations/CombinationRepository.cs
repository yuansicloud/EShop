using EasyAbp.EShop.Plugins.Combinations.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public class CombinationRepository : EfCoreRepository<ICombinationsDbContext, Combination, Guid>, ICombinationRepository
    {
        public CombinationRepository(IDbContextProvider<ICombinationsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<Combination> UpdateAsync(Combination entity, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            await CheckUniqueNameAsync(entity, cancellationToken);

            return await base.UpdateAsync(entity, autoSave, cancellationToken);
        }

        public override async Task<Combination> InsertAsync(Combination entity, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            await CheckUniqueNameAsync(entity, cancellationToken);

            return await base.InsertAsync(entity, autoSave, cancellationToken);
        }

        public virtual async Task CheckUniqueNameAsync(Combination entity, CancellationToken cancellationToken = new CancellationToken())
        {
            if (entity.UniqueName.IsNullOrEmpty())
            {
                return;
            }

            if (await (await GetDbSetAsync()).AnyAsync(
                x => x.UniqueName == entity.UniqueName && x.Id != entity.Id,
                cancellationToken))
            {
                throw new DuplicatedCombinationUniqueNameException(entity.UniqueName);
            }
        }


        public override async Task<IQueryable<Combination>> WithDetailsAsync()
        {
            return (await base.WithDetailsAsync())
                .IncludeDetails();
        }
    }
}