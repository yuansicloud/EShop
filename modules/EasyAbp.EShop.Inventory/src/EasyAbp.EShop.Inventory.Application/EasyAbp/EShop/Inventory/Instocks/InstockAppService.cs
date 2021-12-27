using EasyAbp.EShop.Inventory.Instocks.Dtos;
using EasyAbp.EShop.Inventory.Permissions;
using EasyAbp.EShop.Stores.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Inventory.Instocks
{
    public class InstockAppService : MultiStoreReadOnlyAppService<Instock, InstockDto, Guid, GetInstockListInput>,
        IInstockAppService
    {
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;

        protected override string CrossStorePolicyName { get; set; } = InventoryPermissions.Inventories.CrossStore;

        private readonly IInstockRepository _repository;


        public InstockAppService(
            IInstockRepository repository) : base(repository)
        {
            _repository = repository;
        }
        protected override async Task<IQueryable<Instock>> CreateFilteredQueryAsync(GetInstockListInput input)
        {
            return (await _repository.WithDetailsAsync())
                .WhereIf(!input.Filter.IsNullOrEmpty(), s => s.Description.Contains(input.Filter) || s.InstockNumber.Contains(input.Filter))
                .WhereIf(input.ProductId.HasValue, s => s.ProductId == input.ProductId.Value)
                .WhereIf(input.ProductSkuId.HasValue, s => s.ProductSkuId == input.ProductSkuId.Value)
                .WhereIf(input.StoreId.HasValue, s => s.StoreId == input.StoreId.Value)
                .WhereIf(!input.OperatorName.IsNullOrEmpty(), s => s.OperatorName.Contains(input.OperatorName))
                .WhereIf(input.MinCreationTime.HasValue, s => s.CreationTime.Date >= input.MinCreationTime.Value.Date)
                .WhereIf(input.MaxCreationTime.HasValue, s => s.CreationTime.Date >= input.MaxCreationTime.Value.Date)
                .WhereIf(input.MinInstockTime.HasValue, s => s.InstockTime.Date >= input.MinInstockTime.Value.Date)
                .WhereIf(input.MaxInstockTime.HasValue, s => s.InstockTime.Date <= input.MaxInstockTime.Value.Date);
        }


    }
}
