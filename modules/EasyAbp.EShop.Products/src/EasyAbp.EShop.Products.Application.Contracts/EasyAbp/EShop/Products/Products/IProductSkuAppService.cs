using EasyAbp.EShop.Products.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
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