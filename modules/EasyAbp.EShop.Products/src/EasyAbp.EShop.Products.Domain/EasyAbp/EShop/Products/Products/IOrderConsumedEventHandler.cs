﻿using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Products.Products
{
    public interface IOrderConsumedEventHandler : IDistributedEventHandler<OrderConsumedEto>
    {
        
    }
}