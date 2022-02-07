namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    using EasyAbp.EShop.Plugins.Combinations.CombinationDetails;
    using EasyAbp.EShop.Plugins.Combinations.CombinationDetails.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    [RemoteService(Name = "EasyAbpEShopCombinations")]
    [Route("/api/e-shop/combinations/combination-detail")]
    public class CombinationDetailController : CombinationsController, ICombinationDetailAppService
    {

        private readonly ICombinationDetailAppService _service;

        public CombinationDetailController(ICombinationDetailAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<CombinationDetailDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<CombinationDetailDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public Task<CombinationDetailDto> CreateAsync(CreateCombinationDetailDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<CombinationDetailDto> UpdateAsync(Guid id, UpdateCombinationDetailDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }
    }
}
