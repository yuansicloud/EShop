using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public interface ICombinationRepository : IRepository<Combination, Guid>
    {
        Task CheckUniqueNameAsync(Combination combination, CancellationToken cancellationToken = new CancellationToken());
    }
}