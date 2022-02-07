using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Combinations.CombinationDetails
{
    public interface ICombinationDetailRepository : IRepository<CombinationDetail, Guid>
    {
    }
}