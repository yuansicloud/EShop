using System;
using EasyAbp.EShop.Plugins.Combinations.CombinationDetails.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Combinations.CombinationDetails
{
    public class CombinationDetailAppService : CrudAppService<CombinationDetail, CombinationDetailDto, Guid, PagedAndSortedResultRequestDto, CreateCombinationDetailDto, UpdateCombinationDetailDto>,
        ICombinationDetailAppService
    {

        private readonly ICombinationDetailRepository _repository;
        
        public CombinationDetailAppService(ICombinationDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
