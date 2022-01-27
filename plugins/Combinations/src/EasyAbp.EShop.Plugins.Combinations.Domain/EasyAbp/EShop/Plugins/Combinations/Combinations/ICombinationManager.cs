using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public interface ICombinationManager : IDomainService
    {
        Task<Combination> CreateAsync(Combination combination);

        Task<Combination> UpdateAsync(Combination combination);

        Task DeleteAsync(Combination combination);

        Task<Combination> CreateCombinationItemAsync(Combination combination, CombinationItem combinationItem);

        Task<Combination> UpdateCombinationItemAsync(Combination combination, CombinationItem combinationItem);

        Task<Combination> DeleteCombinationItemAsync(Combination combination, CombinationItem combinationItem);

    }
}
