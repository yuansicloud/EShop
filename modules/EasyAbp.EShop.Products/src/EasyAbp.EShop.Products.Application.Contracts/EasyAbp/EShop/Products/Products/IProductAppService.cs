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
    }
}