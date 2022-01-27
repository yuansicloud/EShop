using System;
using EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public interface ICombinationAppService :
        ICrudAppService< 
            CombinationDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateCombinationDto,
            UpdateCombinationDto>
    {

    }
}