using System;
using EasyAbp.EShop.Inventory.Permissions;
using EasyAbp.EShop.Inventory.Aggregates.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using EasyAbp.EShop.Stores.Stores;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using EasyAbp.EShop.Inventory.Stocks;
using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Outstocks;
using EasyAbp.EShop.Inventory.StockHistories;
using System.Collections.Generic;

namespace EasyAbp.EShop.Inventory.Aggregates
{
    public class AggregateAppService : InventoryAppService, IAggregateAppService
    {

        private readonly IDistributedEventBus _distributedEventBus;

        private readonly IStockManager _stockManager;

        private readonly IStockAppService _stockAppService;

        private readonly IInstockAppService _instockAppService;

        private readonly IOutstockAppService _outstockAppService;

        private readonly IStockHistoryAppService _stockHistoryAppService;

        public AggregateAppService(
            IStockManager stockManager,
            IDistributedEventBus distributedEventBus,
            IStockAppService stockAppService,
            IInstockAppService instockAppService,
            IOutstockAppService outstockAppService,
            IStockHistoryAppService stockHistoryAppService
         ) 
        {
            _stockManager = stockManager;
            _distributedEventBus = distributedEventBus;
            _stockAppService = stockAppService;
            _instockAppService = instockAppService;
            _outstockAppService = outstockAppService;
            _stockHistoryAppService = stockHistoryAppService;
        }

      
        public async Task<ProductStockDetailDto> GetProductStockDetail(Guid productId, DateTime? termStartTime, DateTime? termEndTime)
        {
            var stocks = await _stockAppService.GetListAsync(new Stocks.Dtos.GetStockListInput
            {
                ProductId = productId,
                IsEnabled = true,
                MaxResultCount = int.MaxValue
            });

            var instocks = await _instockAppService.GetListAsync(new Instocks.Dtos.GetInstockListInput {
                ProductId = productId,
                MaxResultCount = int.MaxValue
            });

            var outstocks = await _outstockAppService.GetListAsync(new Outstocks.Dtos.GetOutstockListInput
            {
                ProductId = productId,
                MaxResultCount = int.MaxValue
            });

            var histories = await _stockHistoryAppService.GetListAsync(new StockHistories.Dtos.GetStockHistoryListInput
            {
                ProductId = productId,
                MaxResultCount = int.MaxValue,
                CreationStartTime = termStartTime,
                CreationEndTime = termEndTime
            });

            List<Guid> productSkuIds = stocks.Items.Select(s => s.ProductSkuId).ToList();

            return new ProductStockDetailDto { 
                ProductId = productId,
                ProductSkuStockDetails = productSkuIds.Select(skuId => new ProductSkuStockDetailDto { 
                    Quantity = stocks.Items.Where(p => p.ProductSkuId == skuId).Sum(s => s.Quantity),
                    InstockQuantity = instocks.Items.Where(p => p.ProductSkuId == skuId).Sum(s => s.Units),
                    OutStockQuantity = outstocks.Items.Where(p => p.ProductSkuId == skuId).Sum(s => s.Units),
                    TermStartQuantity = histories.Items.FirstOrDefault()?.Quantity,
                    TermEndQuantity = histories.Items.LastOrDefault()?.Quantity,
                    LockedQuantity = stocks.Items.Where(p => p.ProductSkuId == skuId).Sum(s => s.LockedQuantity),
                    ProductId = productId,
                    ProductSkuId = skuId
                })
                .ToList()
            };
        }

    }
}
