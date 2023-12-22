using System;
using EasyAbp.EShop.Plugins.Combinations.CombinationDetails.Dtos;
using EasyAbp.EShop.Plugins.Combinations.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Combinations.CombinationDetails
{
    public class CombinationDetailAppService : CrudAppService<CombinationDetail, CombinationDetailDto, Guid, PagedAndSortedResultRequestDto, CreateCombinationDetailDto, UpdateCombinationDetailDto>,
        ICombinationDetailAppService
    {
        protected override string CreatePolicyName { get; set; } = CombinationsPermissions.Combinations.Manage;
        protected override string UpdatePolicyName { get; set; } = CombinationsPermissions.Combinations.Manage;
        protected override string DeletePolicyName { get; set; } = CombinationsPermissions.Combinations.Manage;

        private readonly ICombinationDetailRepository _repository;
        
        public CombinationDetailAppService(ICombinationDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
