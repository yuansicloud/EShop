﻿using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Outstocks;
using EasyAbp.EShop.Inventory.Reallocations;
using EasyAbp.EShop.Inventory.StockHistories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Inventory.Stocks
{
    public class StockManager : DomainService, IStockManager
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IStockRepository _stockRepository;
        private readonly IInstockRepository _instockRepository;
        private readonly IOutstockRepository _outstockRepository;

        public StockManager(
            IObjectMapper objectMapper,
            IDistributedEventBus distributedEventBus,
            IUnitOfWorkManager unitOfWorkManager,
            IStockRepository stockRepository)
        {
            _objectMapper = objectMapper;
            _distributedEventBus = distributedEventBus;
            _unitOfWorkManager = unitOfWorkManager;
            _stockRepository = stockRepository;
        }


        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _stockRepository.InsertAsync(stock);

            //TODO: Localize
            await AdjustStock(stock.ProductSkuId, stock.WarehouseId, 0, "初始化库存");

            return stock;
        }

        public async Task<Instock> CreateAsync(Instock instock)
        {
            await _instockRepository.InsertAsync(instock);

            //TODO: Localize
            await AdjustStock(instock.ProductSkuId, instock.WarehouseId, instock.Units, instock.Description);

            return instock;
        }

        public async Task<Outstock> CreateAsync(Outstock outstock)
        {
            await _outstockRepository.InsertAsync(outstock);

            //TODO: Localize
            await AdjustStock(outstock.ProductSkuId, outstock.WarehouseId, -outstock.Units, outstock.Description);

            return outstock;
        }

        public Task<Reallocation> CreateAsync(Reallocation instock)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Stock stock)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Instock instock)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Outstock instock)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Reallocation instock)
        {
            throw new NotImplementedException();
        }

        public Task DeleteInstockAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteInventoryAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOutstockAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteReallocationAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Stock> UpdateAsync(Stock stock)
        {
            throw new NotImplementedException();
        }

        public Task<Instock> UpdateAsync(Instock instock)
        {
            throw new NotImplementedException();
        }

        public Task<Outstock> UpdateAsync(Outstock instock)
        {
            throw new NotImplementedException();
        }

        public Task<Reallocation> UpdateAsync(Reallocation instock)
        {
            throw new NotImplementedException();
        }

        public async Task<Stock> AdjustStock(Guid productSkuId, Guid warehouseId, int adjustedQuantity, string description = null)
        {
            var stock = await _stockRepository.FindAsync(s => s.ProductSkuId == productSkuId && s.WarehouseId == warehouseId);

            if (stock == null)
            {
                //TODO: Encapsulate Exceptions
                throw new UserFriendlyException("存品不存在");
            }

            stock.AdjustQuantity(adjustedQuantity, description);

            return await _stockRepository.UpdateAsync(stock);
        }

        protected async Task CheckStockExist(Guid productSkuId, Guid warehouseId)
        {

        }

    }
}
