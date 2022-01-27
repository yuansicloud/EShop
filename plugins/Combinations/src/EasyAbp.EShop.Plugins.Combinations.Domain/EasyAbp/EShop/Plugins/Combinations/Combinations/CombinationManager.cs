using System.Threading.Tasks;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public class CombinationManager : ICombinationManager
    {
        private readonly ICombinationRepository _combinationRepository;

        public CombinationManager(ICombinationRepository combinationRepository)
        {
            _combinationRepository = combinationRepository;
        }

        public async Task<Combination> CreateAsync(Combination combination)
        {
            combination.TrimUniqueName();

            await CheckCombinationUniqueNameAsync(combination);

            await _combinationRepository.InsertAsync(combination, autoSave: true);

            //await CheckCombinationDetailAvailableAsync(combination.Id, combination.CombinationDetailId);

            return combination;
        }

        public Task<Combination> CreateCombinationItemAsync(Combination combination, CombinationItem combinationItem)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(Combination combination)
        {
            throw new System.NotImplementedException();
        }

        public Task<Combination> DeleteCombinationItemAsync(Combination combination, CombinationItem combinationItem)
        {
            throw new System.NotImplementedException();
        }

        public Task<Combination> UpdateAsync(Combination combination)
        {
            throw new System.NotImplementedException();
        }

        public Task<Combination> UpdateCombinationItemAsync(Combination combination, CombinationItem combinationItem)
        {
            throw new System.NotImplementedException();
        }

        protected virtual async Task CheckCombinationUniqueNameAsync(Combination combination)
        {
            await _combinationRepository.CheckUniqueNameAsync(combination);
        }
    }
}
