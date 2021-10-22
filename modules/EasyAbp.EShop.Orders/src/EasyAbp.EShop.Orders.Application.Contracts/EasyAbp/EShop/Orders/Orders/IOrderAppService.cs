using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderAppService :
        ICrudAppService< 
            OrderDto, 
            Guid, 
            GetOrderListDto,
            CreateOrderDto>
    {
        Task<OrderDto> GetByOrderNumberAsync(string orderNumber);
        
        Task<OrderDto> CompleteAsync(Guid id);

        Task<OrderDto> CancelAsync(Guid id, CancelOrderInput input);

        Task<List<OrderDto>> CreateInBulk(List<CreateOrderDto> input);

        Task<OrderDto> CreateByImport(CreateOrderDto input);
    }
}