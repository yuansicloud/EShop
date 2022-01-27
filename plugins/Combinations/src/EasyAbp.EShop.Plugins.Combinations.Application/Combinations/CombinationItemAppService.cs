using System;
using EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public class CombinationItemAppService : CrudAppService<CombinationItem, CombinationItemDto, Guid, PagedAndSortedResultRequestDto, CreateCombinationItemDto, UpdateCombinationItemDto>,
        ICombinationItemAppService
    {

        private readonly ICombinationItemRepository _repository;
        
        public CombinationItemAppService(ICombinationItemRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
