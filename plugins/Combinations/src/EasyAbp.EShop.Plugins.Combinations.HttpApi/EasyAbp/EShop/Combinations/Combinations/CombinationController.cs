namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    using EasyAbp.EShop.Plugins.Combinations;
    using EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    [RemoteService(Name = "EasyAbpEShopCombinations")]
    [Route("/api/e-shop/plugins/combinations/combination")]
    public class CombinationController : CombinationsController, ICombinationAppService
    {

        private readonly ICombinationAppService _service;

        public CombinationController(ICombinationAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public Task<PagedResultDto<CombinationDto>> GetListAsync(GetCombinationListInput input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public Task<CombinationDto> CreateAsync(CreateCombinationDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<CombinationDto> UpdateAsync(Guid id, UpdateCombinationDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<CombinationDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpPost]
        [Route("{id}/item")]
        public Task<CombinationDto> CreateCombinationItemAsync(Guid id, CreateCombinationItemDto input)
        {
            return _service.CreateCombinationItemAsync(id, input);
        }

        [HttpPut]
        [Route("{id}/item/{combinationItemId}")]
        public Task<CombinationDto> UpdateCombinationItemAsync(Guid id, Guid combinationItemId, UpdateCombinationItemDto input)
        {
            return _service.UpdateCombinationItemAsync(id, combinationItemId, input);
        }

        [HttpDelete]
        [Route("{id}/item/{combinationItemId}")]
        public Task<CombinationDto> DeleteCombinationItemAsync(Guid id, Guid combinationItemId)
        {
            return _service.DeleteCombinationItemAsync(id, combinationItemId);
        }

        [HttpGet]
        [Route("find-by-id/{id}")]
        public Task<CombinationDto> FindAsync(Guid id)
        {
            return _service.FindAsync(id);
        }
    }
}
