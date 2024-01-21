using EasyAbp.EShop.Inventory.Instocks.Dtos;
using EasyAbp.EShop.Inventory.Outstocks.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Outstocks
{
    public interface IOutstockAppService :
        IReadOnlyAppService< 
            OutstockDto, 
            Guid,
            GetOutstockListInput>
    {
        Task UpdateOutstock(Guid id, UpdateOutstockDto input);
    }
}