using System;
using EasyAbp.EShop.Inventory.Permissions;
using EasyAbp.EShop.Inventory.Outstocks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using EasyAbp.EShop.Stores.Stores;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Inventory.Stocks;
using System.Collections.Generic;
using Volo.Abp;

namespace EasyAbp.EShop.Inventory.Outstocks
{
    public class OutstockAppService : MultiStoreCrudAppService<Outstock, OutstockDto, Guid, GetOutstockListInput, OutstockCreateDto, OutstockUpdateDto>,
        IOutstockAppService
    {
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;
        protected override string CreatePolicyName { get; set; } = InventoryPermissions.Stock.Create;
        protected override string UpdatePolicyName { get; set; } = InventoryPermissions.Stock.Update;
        protected override string DeletePolicyName { get; set; } = InventoryPermissions.Stock.Delete;
        protected override string CrossStorePolicyName { get; set; } = InventoryPermissions.Stock.CrossStore;

        private readonly IOutstockRepository _repository;

        private readonly IStockManager _stockManager;

        public OutstockAppService(
            IOutstockRepository repository, 
            IStockManager stockManager) : base(repository)
        {
            _repository = repository;
            _stockManager = stockManager;
        }


        public override async Task<OutstockDto> CreateAsync(OutstockCreateDto input)
        {
            await MapOutstockNumber(input);

            var entity = await MapToEntityAsync(input);

            TryToSetTenantId(entity);

            return await MapToGetOutputDtoAsync(
                await _stockManager.CreateAsync(entity));

        }

        public async Task MultiCreateAsync(List<OutstockCreateDto> input)
        {
            var instockNumber = await GenerateOutstockNumber();

            foreach (var dto in input)
            {
                await MapOutstockNumber(dto, instockNumber);

                await CreateAsync(dto);
            }
        }

        protected override async Task<IQueryable<Outstock>> CreateFilteredQueryAsync(GetOutstockListInput input)
        {
            return (await _repository.WithDetailsAsync())
                .WhereIf(!input.Filter.IsNullOrEmpty(), s => s.Description.Contains(input.Filter) || s.OutstockNumber.Contains(input.Filter))
                .WhereIf(input.ProductSkuId.HasValue, s => s.ProductSkuId == input.ProductSkuId.Value)
                .WhereIf(input.WarehouseId.HasValue, s => s.WarehouseId == input.WarehouseId.Value)
                .WhereIf(input.StoreId.HasValue, s => s.StoreId == input.StoreId.Value)
                .WhereIf(input.CreationStartTime.HasValue, s => s.CreationTime.Date >= input.CreationStartTime.Value.Date)
                .WhereIf(input.CreationEndTime.HasValue, s => s.CreationTime.Date >= input.CreationEndTime.Value.Date)
                .WhereIf(input.OutstockStartTime.HasValue, s => s.OutstockTime.Date >= input.OutstockStartTime.Value.Date)
                .WhereIf(input.OutstockEndTime.HasValue, s => s.OutstockTime.Date >= input.OutstockEndTime.Value.Date);
        }

        protected virtual async Task<OutstockCreateDto> MapOutstockNumber(OutstockCreateDto dto, string instockNumber = null)
        {
            if (dto.OutstockNumber.IsNullOrEmpty())
            {
                dto.OutstockNumber = instockNumber ?? await GenerateOutstockNumber();
            }

            return dto;
        }

        protected virtual Task<string> GenerateOutstockNumber()
        {
            return Task.FromResult("CK" + Clock.Now.ToString("yyyyMMddHHmmssffff") + RandomHelper.GetRandom(0, 99).ToString("00"));
        }
    }
}
