using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products
{
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/eShop/products/product")]
    public class ProductController : ProductsController, IProductAppService
    {
        private readonly IProductAppService _service;

        public ProductController(IProductAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public Task<PagedResultDto<ProductWithExtraDataDto>> GetListAsync(GetProductListDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public Task<ProductWithExtraDataDto> CreateAsync(CreateUpdateProductDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<ProductWithExtraDataDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id, Guid storeId)
        {
            return _service.DeleteAsync(id, storeId);
        }

        [HttpPost]
        [Route("{id}/sku")]
        public Task<ProductWithExtraDataDto> CreateSkuAsync(Guid id, Guid storeId, CreateProductSkuDto input)
        {
            return _service.CreateSkuAsync(id, storeId, input);
        }

        [HttpPut]
        [Route("{id}/sku/{productSkuId}")]
        public Task<ProductWithExtraDataDto> UpdateSkuAsync(Guid id, Guid productSkuId, Guid storeId, UpdateProductSkuDto input)
        {
            return _service.UpdateSkuAsync(id, productSkuId, storeId, input);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ProductWithExtraDataDto> GetAsync(Guid id, Guid storeId)
        {
            return _service.GetAsync(id, storeId);
        }

        [HttpGet]
        [Route("byUniqueName/{storeId}")]
        public Task<ProductWithExtraDataDto> GetByUniqueNameAsync(string code, Guid storeId)
        {
            return _service.GetByUniqueNameAsync(code, storeId);
        }

        [HttpDelete]
        [Route("{id}/sku/{productSkuId}")]
        public Task<ProductWithExtraDataDto> DeleteSkuAsync(Guid id, Guid productSkuId, Guid storeId)
        {
            return _service.DeleteSkuAsync(id, productSkuId, storeId);
        }

        [HttpGet]
        [Route("productGroup")]
        public Task<ListResultDto<ProductGroupDto>> GetProductGroupListAsync()
        {
            return _service.GetProductGroupListAsync();
        }
    }
}