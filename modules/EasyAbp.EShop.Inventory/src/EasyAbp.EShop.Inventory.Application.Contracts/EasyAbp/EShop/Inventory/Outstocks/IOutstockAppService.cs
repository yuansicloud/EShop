using EasyAbp.EShop.Inventory.Outstocks.Dtos;
using System;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Outstocks
{
    public interface IOutstockAppService :
        IReadOnlyAppService< 
            OutstockDto, 
            Guid,
            GetOutstockListInput>
    {
    }
}