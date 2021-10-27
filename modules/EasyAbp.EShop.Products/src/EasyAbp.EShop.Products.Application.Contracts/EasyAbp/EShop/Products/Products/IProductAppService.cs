using EasyAbp.EShop.Products.Products.Dtos;
using System;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductSkuAppService :
        IReadOnlyAppService<
            ProductSkuDto,
            Guid,
            GetProductSkuListInput>
    {
        Task<ProductDto> CreateSkuAsync(Guid productId, CreateProductSkuDto input);

        Task<ProductDto> UpdateSkuAsync(Guid productId, Guid productSkuId, UpdateProductSkuDto input);

        Task<ProductDto> DeleteSkuAsync(Guid productId, Guid productSkuId);
        
        Task<ProductDto> GetByUniqueNameAsync(Guid storeId, string uniqueName);

        Task<ListResultDto<ProductGroupDto>> GetProductGroupListAsync();
    }
}