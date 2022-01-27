using System;
using EasyAbp.EShop.Plugins.Combinations.Permissions;
using EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using EasyAbp.EShop.Stores.Stores;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public class CombinationAppService : MultiStoreCrudAppService<Combination, CombinationDto, Guid, PagedAndSortedResultRequestDto, CreateCombinationDto, UpdateCombinationDto>,
        ICombinationAppService
    {
        protected override string GetPolicyName { get; set; } = CombinationsPermissions.Combination.Default;
        protected override string GetListPolicyName { get; set; } = CombinationsPermissions.Combination.Default;
        protected override string CreatePolicyName { get; set; } = CombinationsPermissions.Combination.Create;
        protected override string UpdatePolicyName { get; set; } = CombinationsPermissions.Combination.Update;
        protected override string DeletePolicyName { get; set; } = CombinationsPermissions.Combination.Delete;

        protected override string CrossStorePolicyName { get; set; } = CombinationsPermissions.Combination.CrossStore;

        private readonly ICombinationRepository _repository;

        private readonly ICombinationManager _combinationManager;

        public CombinationAppService(
            ICombinationRepository repository, 
            ICombinationManager combinationManager) : base(repository)
        {
            _repository = repository;
            _combinationManager = combinationManager;
        }

        public override async Task<CombinationDto> CreateAsync(CreateCombinationDto input)
        {
            var combination = MapToEntity(input);

            await CheckMultiStorePolicyAsync(combination.StoreId, CreatePolicyName);

            TryToSetTenantId(combination);

            await _combinationManager.CreateAsync(combination);

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }
    }
}
