using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public interface ICombinationItemRepository : IRepository<CombinationItem, Guid>
    {
    }
}