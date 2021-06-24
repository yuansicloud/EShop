using EasyAbp.EShop.Orders.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Products.Products
{
    public interface IOrderCancelledEventHandler : IDistributedEventHandler<OrderCanceledEto>
    {
    }
}
