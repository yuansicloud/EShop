using EasyAbp.EShop.Inventory.Instocks.Dtos;
using System;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Instocks
{
    public interface IInstockAppService :
        IReadOnlyAppService< 
            InstockDto, 
            Guid,
            GetInstockListInput>
    {
    }
}