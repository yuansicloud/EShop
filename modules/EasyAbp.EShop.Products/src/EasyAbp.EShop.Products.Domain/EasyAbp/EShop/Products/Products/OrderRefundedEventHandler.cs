﻿using EasyAbp.EShop.Orders.Orders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products
{
    public class OrderRefundedEventHandler : IOrderRefundedEventHandler, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IProductRepository _productRepository;
        private readonly IProductManager _productManager;

        public OrderRefundedEventHandler(
            ICurrentTenant currentTenant,
            IUnitOfWorkManager unitOfWorkManager,
            IDistributedEventBus distributedEventBus,
            IProductRepository productRepository,
            IProductManager productManager)
        {
            _currentTenant = currentTenant;
            _unitOfWorkManager = unitOfWorkManager;
            _distributedEventBus = distributedEventBus;
            _productRepository = productRepository;
            _productManager = productManager;
        }

        public virtual async Task HandleEventAsync(OrderRefundedEto eventData)
        {
            using var uow = _unitOfWorkManager.Begin(isTransactional: true);

            using var changeTenant = _currentTenant.Change(eventData.Order.TenantId);

            var models = new List<ConsumeInventoryModel>();

            foreach (var orderLine in eventData.Order.OrderLines)
            {
                // Todo: Should use ProductHistory.
                var product = await _productRepository.FindAsync(orderLine.ProductId);

                var productSku = product?.ProductSkus.FirstOrDefault(sku => sku.Id == orderLine.ProductSkuId);

                if (productSku == null)
                {
                    //await PublishResultEventAsync(eventData, false);

                    continue;
                }

                if (product.InventoryStrategy != InventoryStrategy.ReduceAfterPayment && product.InventoryStrategy != InventoryStrategy.ReduceAfterPlacing)
                {
                    continue;
                }

                //if (!await _productManager.IsInventorySufficientAsync(product, productSku, orderLine.Quantity))
                //{
                //    //await PublishResultEventAsync(eventData, false);

                //    return;
                //}

                var model = new ConsumeInventoryModel
                {
                    Product = product,
                    ProductSku = productSku,
                    StoreId = eventData.Order.StoreId,
                    Quantity = orderLine.RefundedQuantity,
                    ExtraProperties = new()
                };

                model.ExtraProperties.Add("UnitPrice", orderLine.RefundAmount / orderLine.RefundedQuantity);
                model.ExtraProperties.Add("Description", "订单退款返库存");
                model.ExtraProperties.Add("InstockType", 4.ToString());
                model.ExtraProperties.Add("OperatorName", "系统自动");
                models.Add(model);
            }

            foreach (var model in models)
            {
                if (await _productManager.TryIncreaseInventoryAsync(model.Product, model.ProductSku, model.Quantity, true, model.ExtraProperties))
                {
                    continue;
                }

                await uow.RollbackAsync();

                //await PublishResultEventAsync(eventData, false);

                return;
            }

            await uow.CompleteAsync();

            //await PublishResultEventAsync(eventData, true);
        }

        //protected virtual async Task PublishResultEventAsync(OrderPaidEto orderPaidEto, bool isSuccess)
        //{
        //    await _distributedEventBus.PublishAsync(new ProductInventoryReductionAfterOrderPaidResultEto
        //    {
        //        TenantId = orderPaidEto.Order.TenantId,
        //        OrderId = orderPaidEto.Order.Id,
        //        PaymentId = orderPaidEto.PaymentId,
        //        PaymentItemId = orderPaidEto.PaymentItemId,
        //        IsSuccess = isSuccess
        //    });
        //}
    }
}