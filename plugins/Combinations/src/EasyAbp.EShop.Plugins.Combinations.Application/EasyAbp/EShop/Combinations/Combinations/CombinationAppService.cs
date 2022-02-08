using EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos;
using EasyAbp.EShop.Plugins.Combinations.Permissions;
using EasyAbp.EShop.Stores.Authorization;
using EasyAbp.EShop.Stores.Stores;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public class CombinationAppService : MultiStoreCrudAppService<Combination, CombinationDto, Guid, GetCombinationListInput, CreateCombinationDto, UpdateCombinationDto>,
        ICombinationAppService
    {
        protected override string GetPolicyName { get; set; } = CombinationsPermissions.Combinations.Default;
        protected override string GetListPolicyName { get; set; } = CombinationsPermissions.Combinations.Default;
        protected override string CreatePolicyName { get; set; } = CombinationsPermissions.Combinations.Manage;
        protected override string UpdatePolicyName { get; set; } = CombinationsPermissions.Combinations.Manage;
        protected override string DeletePolicyName { get; set; } = CombinationsPermissions.Combinations.Manage;

        protected override string CrossStorePolicyName { get; set; } = CombinationsPermissions.Combinations.CrossStore;

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

        public override async Task<CombinationDto> UpdateAsync(Guid id, UpdateCombinationDto input)
        {
            var combination = await GetEntityByIdAsync(id);

            await CheckMultiStorePolicyAsync(combination.StoreId, UpdatePolicyName);

            if (input.StoreId != combination.StoreId)
            {
                await CheckMultiStorePolicyAsync(input.StoreId, UpdatePolicyName);
            }

            MapToEntity(input, combination);

            await _combinationManager.UpdateAsync(combination);

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }

        public override async Task<CombinationDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            var combination = await GetEntityByIdAsync(id);

            if (!combination.IsPublished)
            {
                await CheckMultiStorePolicyAsync(combination.StoreId, CombinationsPermissions.Combinations.Manage);
            }

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }

        public async Task<CombinationDto> FindAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            var combination = await _repository.FindAsync(id);

            if (combination is null)
            {
                return null;
            }

            if (!combination.IsPublished)
            {
                await CheckMultiStorePolicyAsync(combination.StoreId, CombinationsPermissions.Combinations.Manage);
            }

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }

        public async Task<CombinationDto> CreateCombinationItemAsync(Guid id, CreateCombinationItemDto input)
        {
            //TODO: Add Store Validation

            var combination = await GetEntityByIdAsync(id);

            await CheckMultiStorePolicyAsync(combination.StoreId, UpdatePolicyName);

            var item = ObjectMapper.Map<CreateCombinationItemDto, CombinationItem>(input);

            EntityHelper.TrySetId(item, GuidGenerator.Create);

            await _combinationManager.CreateCombinationItemAsync(combination, item);

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }

        public async Task<CombinationDto> UpdateCombinationItemAsync(Guid id, Guid combinationItemId, UpdateCombinationItemDto input)
        {
            var combination = await GetEntityByIdAsync(id);

            await CheckMultiStorePolicyAsync(combination.StoreId, UpdatePolicyName);

            var sku = combination.CombinationItems.Single(x => x.Id == combinationItemId);

            ObjectMapper.Map(input, sku);

            await _combinationManager.UpdateCombinationItemAsync(combination, sku);

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }

        public async Task<CombinationDto> DeleteCombinationItemAsync(Guid id, Guid combinationItemId)
        {
            var combination = await GetEntityByIdAsync(id);

            await CheckMultiStorePolicyAsync(combination.StoreId, UpdatePolicyName);

            var sku = combination.CombinationItems.Single(x => x.Id == combinationItemId);

            await _combinationManager.DeleteCombinationItemAsync(combination, sku);

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }

        public virtual async Task<CombinationDto> ChangeCombinationPublished(Guid id, ChangeCombinationPublishedDto input)
        {
            var combination = await GetEntityByIdAsync(id);

            await CheckMultiStorePolicyAsync(combination.StoreId, UpdatePolicyName);

            combination.TogglePublished(input.IsPublished);

            await _repository.UpdateAsync(combination, autoSave: true);

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }
        protected override async Task<IQueryable<Combination>> CreateFilteredQueryAsync(GetCombinationListInput input)
        {
            var query = await _repository.WithDetailsAsync();

            var isCurrentUserStoreAdmin = await AuthorizationService.IsMultiStoreGrantedAsync(input.StoreId, CombinationsPermissions.Combinations.Default, CombinationsPermissions.Combinations.CrossStore);

            if (!isCurrentUserStoreAdmin)
            {
                query = query.Where(x => x.IsPublished);
            }

            return query
                .WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId)
                .WhereIf(!input.Filter.IsNullOrEmpty(), x => x.DisplayName.Contains(input.Filter))
                .WhereIf(input.MinimumPrice.HasValue, x => x.CombinationItems.Sum(x => x.UnitPrice * x.Quantity - x.TotalDiscount) >= input.MinimumPrice.Value)
                .WhereIf(input.MaximumPrice.HasValue, x => x.CombinationItems.Sum(x => x.UnitPrice * x.Quantity - x.TotalDiscount) <= input.MaximumPrice.Value);
        }
    }
}
