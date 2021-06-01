using EasyAbp.EShop.Inventory.Aggregates.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Inventory.Aggregates
{
    public interface IAggregateAppService
    {
        Task<ProductStockDetailDto> GetProductStockDetail(Guid productId, DateTime? termStartTime, DateTime? termEndTime);
    }
}
