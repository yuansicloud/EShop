using System;
using System.Collections.Generic;
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

            await CheckCombinationDetailAvailableAsync(combination.Id, combination.CombinationDetailId);

            return combination;

        }

        public async Task<Combination> CreateCombinationItemAsync(Combination combination, CombinationItem combinationItem)
        {
            //combinationItem.UpdateTotalPrice();

            combination.CombinationItems.AddIfNotContains(combinationItem);

            return await _combinationRepository.UpdateAsync(combination, true);
        }

        public async Task DeleteAsync(Combination combination)
        {
            await _combinationRepository.DeleteAsync(combination, true);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _combinationRepository.DeleteAsync(id, true);
        }

        public async Task<Combination> DeleteCombinationItemAsync(Combination combination, CombinationItem combinationItem)
        {
            combination.CombinationItems.Remove(combinationItem);

            return await _combinationRepository.UpdateAsync(combination, true);
        }

        public async Task<Combination> UpdateAsync(Combination combination)
        {
            await CheckCombinationUniqueNameAsync(combination);

            return await _combinationRepository.UpdateAsync(combination, true);
        }

        public async Task<Combination> UpdateCombinationItemAsync(Combination combination, CombinationItem combinationItem)
        {
            //combinationItem.UpdateTotalPrice();

            return await _combinationRepository.UpdateAsync(combination, true);
        }

        protected virtual async Task CheckCombinationUniqueNameAsync(Combination combination)
        {
            await _combinationRepository.CheckUniqueNameAsync(combination);
        }

        protected virtual async Task CheckCombinationDetailAvailableAsync(Guid currentCombinationId, Guid desiredCombinationDetailId)
        {
            var otherOwner = await _combinationRepository.FindAsync(x =>
                x.CombinationDetailId == desiredCombinationDetailId && x.Id != currentCombinationId);

            // Todo: should also check ProductSku owner

            if (otherOwner != null)
            {
                throw new CombinationDetailHasBeenUsedException(desiredCombinationDetailId);
            }
        }

    }
}
