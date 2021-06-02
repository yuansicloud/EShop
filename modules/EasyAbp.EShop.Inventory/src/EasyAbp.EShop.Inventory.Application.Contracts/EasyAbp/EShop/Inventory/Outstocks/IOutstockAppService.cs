using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Inventory.Outstocks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Outstocks
{
    public interface IOutstockAppService :
        ICrudAppService< 
            OutstockDto, 
            Guid,
            GetOutstockListInput,
            OutstockCreateDto,
            OutstockUpdateDto>
    {
        Task MultiCreateAsync(List<OutstockCreateDto> input);
    }
}