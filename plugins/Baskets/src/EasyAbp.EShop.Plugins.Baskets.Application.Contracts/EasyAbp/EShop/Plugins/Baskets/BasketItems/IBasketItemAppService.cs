using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public interface IBasketItemAppService :
        ICrudAppService< 
            BasketItemDto, 
            Guid, 
            GetBasketItemListDto,
            CreateBasketItemDto,
            UpdateBasketItemDto>
    {
        Task<List<BasketItemDto>> CreateInBulkAsync(List<CreateBasketItemDto> input);

        Task DeleteInBulkAsync(IEnumerable<Guid> ids);

        Task CreateOrderFromBasket(CreateOrderFromBasketInput input);
    }
}