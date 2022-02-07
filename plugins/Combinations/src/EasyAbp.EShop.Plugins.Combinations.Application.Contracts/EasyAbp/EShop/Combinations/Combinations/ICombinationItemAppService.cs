using System;
using EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public interface ICombinationItemAppService :
        ICrudAppService< 
            CombinationItemDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateCombinationItemDto,
            UpdateCombinationItemDto>
    {

    }
}