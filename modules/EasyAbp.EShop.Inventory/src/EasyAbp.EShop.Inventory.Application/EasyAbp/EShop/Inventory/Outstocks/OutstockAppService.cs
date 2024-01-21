using EasyAbp.EShop.Inventory.Outstocks.Dtos;
using EasyAbp.EShop.Inventory.Permissions;
using EasyAbp.EShop.Stores.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

namespace EasyAbp.EShop.Inventory.Outstocks
{
    public class OutstockAppService : MultiStoreReadOnlyAppService<Outstock, OutstockDto, Guid, GetOutstockListInput>,
        IOutstockAppService
    {
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;

        protected override string CrossStorePolicyName { get; set; } = InventoryPermissions.Inventories.CrossStore;

        private readonly IOutstockRepository _repository;


        public OutstockAppService(
            IOutstockRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override async Task<IQueryable<Outstock>> CreateFilteredQueryAsync(GetOutstockListInput input)
        {
            return (await _repository.WithDetailsAsync())
                .WhereIf(!input.Filter.IsNullOrEmpty(), s => s.Description.Contains(input.Filter) || s.OutstockNumber.Contains(input.Filter))
                .WhereIf(input.ProductSkuId.HasValue, s => s.ProductSkuId == input.ProductSkuId.Value)
                .WhereIf(input.ProductId.HasValue, s => s.ProductId == input.ProductId.Value)
                .WhereIf(input.StoreId.HasValue, s => s.StoreId == input.StoreId.Value)
                .WhereIf(input.MinCreationTime.HasValue, s => s.CreationTime.Date >= input.MinCreationTime.Value.Date)
                .WhereIf(input.MaxCreationTime.HasValue, s => s.CreationTime.Date >= input.MaxCreationTime.Value.Date)
                .WhereIf(input.MinOutstockTime.HasValue, s => s.OutstockTime.Date >= input.MinOutstockTime.Value.Date)
                .WhereIf(input.MaxOutstockTime.HasValue, s => s.OutstockTime.Date <= input.MaxOutstockTime.Value.Date)
                .WhereIf(input.OutstockType.HasValue, s => s.OutstockType == input.OutstockType.Value)
                .WhereIf(!input.OperatorName.IsNullOrEmpty(), s => s.OperatorName.Contains(input.OperatorName));
        }

        public async Task UpdateOutstock(Guid id, UpdateOutstockDto input)
        {
            var record = await _repository.GetAsync(id);

            record.Update(input.UnitPrice, input.OutstockTime, input.OutstockType, input.OperatorName, input.Description);

            await _repository.UpdateAsync(record);
        }
    }
}
