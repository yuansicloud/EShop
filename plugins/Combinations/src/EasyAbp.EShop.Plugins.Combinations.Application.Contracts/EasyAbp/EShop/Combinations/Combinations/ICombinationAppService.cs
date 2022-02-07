using EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public interface ICombinationAppService :
        ICrudAppService< 
            CombinationDto, 
            Guid,
            GetCombinationListInput,
            CreateCombinationDto,
            UpdateCombinationDto>
    {
        Task<CombinationDto> FindAsync(Guid id);

        Task<CombinationDto> CreateCombinationItemAsync(Guid combinationId, CreateCombinationItemDto input);

        Task<CombinationDto> UpdateCombinationItemAsync(Guid combinationId, Guid combinationItemId, UpdateCombinationItemDto input);

        Task<CombinationDto> DeleteCombinationItemAsync(Guid combinationId, Guid combinationItemId);
    }
}