using EasyAbp.EShop.Inventory.Instocks.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Instocks
{
    public interface IInstockAppService :
        IReadOnlyAppService< 
            InstockDto, 
            Guid,
            GetInstockListInput>
    {
        Task UpdateInstock(Guid id, UpdateInstockDto input);
    }
}