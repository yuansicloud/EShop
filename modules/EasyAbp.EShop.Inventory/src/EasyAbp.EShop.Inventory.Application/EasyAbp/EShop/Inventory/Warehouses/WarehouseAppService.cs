using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Inventory.Permissions;
using EasyAbp.EShop.Inventory.Warehouses.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Warehouses
{
    public class WarehouseAppService : MultiStoreCrudAppService<Warehouse, WarehouseDto, Guid, GetWarehouseListInput, WarehouseCreateDto, WarehouseUpdateDto>,
        IWarehouseAppService
    {
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;
        protected override string CreatePolicyName { get; set; } = InventoryPermissions.Warehouse.Create;
        protected override string UpdatePolicyName { get; set; } = InventoryPermissions.Warehouse.Update;
        protected override string DeletePolicyName { get; set; } = InventoryPermissions.Warehouse.Delete;

        private readonly IWarehouseRepository _repository;
        
        public WarehouseAppService(IWarehouseRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override async Task<IQueryable<Warehouse>> CreateFilteredQueryAsync(GetWarehouseListInput input)
        {
            return (await _repository.WithDetailsAsync())
                .WhereIf(!input.Filter.IsNullOrEmpty(), s => s.Description.Contains(input.Filter) || s.Name.Contains(input.Filter) || s.ContactName.Contains(input.Filter))
                .WhereIf(input.StoreId.HasValue, s => s.StoreId == input.StoreId.Value);
        }
    }
}
