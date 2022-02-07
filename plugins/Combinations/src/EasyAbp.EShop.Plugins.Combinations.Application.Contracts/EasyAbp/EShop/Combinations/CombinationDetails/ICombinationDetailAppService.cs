using System;
using EasyAbp.EShop.Plugins.Combinations.CombinationDetails.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Combinations.CombinationDetails
{
    public interface ICombinationDetailAppService :
        ICrudAppService< 
            CombinationDetailDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateCombinationDetailDto,
            UpdateCombinationDetailDto>
    {

    }
}