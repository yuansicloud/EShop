﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using EasyAbp.EShop.Stores.Permissions;
using EasyAbp.EShop.Stores.StoreOwners;
using EasyAbp.EShop.Stores.Stores.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Stores.Stores
{
    public class StoreAppService : CrudAppService<Store, StoreDto, Guid, GetStoreListInput, CreateUpdateStoreDto, CreateUpdateStoreDto>, IStoreAppService
    {
        protected override string CreatePolicyName { get; set; } = StoresPermissions.Stores.Create;
        protected override string DeletePolicyName { get; set; } = StoresPermissions.Stores.Delete;
        protected override string UpdatePolicyName { get; set; } = StoresPermissions.Stores.Update;
        protected override string GetPolicyName { get; set; } = StoresPermissions.Stores.Default;
        protected override string GetListPolicyName { get; set; } = StoresPermissions.Stores.Default;

        private readonly IPermissionChecker _permissionChecker;
        private readonly IStoreRepository _repository;
        private readonly IStoreOwnerAppService _storeOwnerAppService;
        public StoreAppService(
            IPermissionChecker permissionChecker,
            IStoreRepository repository,
            IStoreOwnerAppService storeOwnerAppService) : base(repository)
        {
            _permissionChecker = permissionChecker;
            _repository = repository;
            _storeOwnerAppService = storeOwnerAppService;
        }

        public override async Task<StoreDto> GetAsync(Guid id)
        {
            return await FillStoreOwners(await base.GetAsync(id));
        }

        public override async Task<StoreDto> CreateAsync(CreateUpdateStoreDto input)
        {
            var store = await base.CreateAsync(input);

            return await UpdateStoreOwners(store, input.StoreOwnerIds);
        }

        public override async Task<StoreDto> UpdateAsync(Guid id, CreateUpdateStoreDto input)
        {
            var store = await base.UpdateAsync(id, input);

            return await UpdateStoreOwners(store, input.StoreOwnerIds);
        }

        protected async Task<StoreDto> FillStoreOwners(StoreDto store)
        {
            store.StoreOwners = (await _storeOwnerAppService.GetListAsync(new StoreOwners.Dtos.GetStoreOwnerListDto
            {
                MaxResultCount = 999,
                StoreId = store.Id
            })).Items.ToList();

            return store;
        }

        protected async Task<StoreDto> UpdateStoreOwners(StoreDto store, List<Guid> ownerIds)
        {
            var currentOwners = (await _storeOwnerAppService.GetListAsync(new StoreOwners.Dtos.GetStoreOwnerListDto
            {
                MaxResultCount = 999,
                StoreId = store.Id
            })).Items;

            var addOwners = ownerIds.Where(o => !currentOwners.Select(o => o.OwnerUserId).Contains(o));

            var deleteOwners = currentOwners.Where(o => !ownerIds.Contains(o.OwnerUserId));

            foreach (var owner in deleteOwners)
            {
                await _storeOwnerAppService.DeleteAsync(owner.Id);
            }

            foreach (var owner in addOwners)
            {
                await _storeOwnerAppService.CreateAsync(new StoreOwners.Dtos.CreateUpdateStoreOwnerDto { 
                    OwnerUserId = owner,
                    StoreId = store.Id
                });
            }

            return await FillStoreOwners(store);
        }

        protected override async Task<IQueryable<Store>> CreateFilteredQueryAsync(GetStoreListInput input)
        {
            if (!input.OnlyManageable || await _permissionChecker.IsGrantedAsync(StoresPermissions.Stores.CrossStore))
            {
                return _repository.AsQueryable();
            }

            return await _repository.GetQueryableOnlyOwnStoreAsync(CurrentUser.GetId());
        }

        public override async Task<PagedResultDto<StoreDto>> GetListAsync(GetStoreListInput input)
        {
            await CheckGetListPolicyAsync();

            var query = await CreateFilteredQueryAsync(input);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncExecuter.ToListAsync(query);
            var entityDtos = await MapToGetListOutputDtosAsync(entities);

            foreach (var dto in entityDtos)
            {
                await FillStoreOwners(dto);
            }

            return new PagedResultDto<StoreDto>(
                totalCount,
                entityDtos
            );
        }

        public async Task<StoreDto> GetDefaultAsync()
        {
            // Todo: need to be improved
            return await FillStoreOwners(ObjectMapper.Map<Store, StoreDto>(await _repository.FindDefaultStoreAsync()));
        }
    }
}