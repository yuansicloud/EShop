using EasyAbp.EShop.Inventory.Stocks.Dtos;
using System;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Inventory.Stocks
{
    public interface IStockAppService :
        ICrudAppService<
            StockDto,
            Guid,
            GetStockListInput,
            StockCreateDto,
            StockUpdateDto>
    {
    }
}