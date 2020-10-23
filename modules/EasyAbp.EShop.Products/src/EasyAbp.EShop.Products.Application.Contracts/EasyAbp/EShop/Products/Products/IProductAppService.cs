using EasyAbp.EShop.Products.Products.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductAppService : IApplicationService
    {
        Task<PagedResultDto<ProductWithExtraDataDto>> GetListAsync(GetProductListDto input);
        
        Task<ProductWithExtraDataDto> CreateAsync(CreateUpdateProductDto input);
        
        Task<ProductWithExtraDataDto> UpdateAsync(Guid id, CreateUpdateProductDto input);
        
        Task DeleteAsync(Guid id, Guid storeId);

        Task<ProductWithExtraDataDto> CreateSkuAsync(Guid productId, Guid storeId, CreateProductSkuDto input);

        Task<ProductWithExtraDataDto> UpdateSkuAsync(Guid productId, Guid productSkuId, Guid storeId, UpdateProductSkuDto input);

        Task<ProductWithExtraDataDto> GetAsync(Guid id, Guid storeId);

        Task<ProductWithExtraDataDto> GetByUniqueNameAsync(string uniqueName, Guid storeId);

        Task<ProductWithExtraDataDto> DeleteSkuAsync(Guid productId, Guid productSkuId, Guid storeId);

        Task<ListResultDto<ProductGroupDto>> GetProductGroupListAsync();
    }
}