using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{
    public class Order : FullAuditedAggregateRoot<Guid>, IOrder, IMultiTenant
    {
        public const string ExtraFeeListPropertyName = "ExtraFeeList";

        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid StoreId { get; protected set; }

        [NotNull]
        public virtual string OrderNumber { get; protected set; }

        public virtual Guid CustomerUserId { get; protected set; }

        public virtual OrderStatus OrderStatus { get; protected set; }

        [NotNull]
        public virtual string Currency { get; protected set; }

        public virtual decimal ProductTotalPrice { get; protected set; }

        public virtual decimal TotalDiscount { get; protected set; }

        public virtual decimal TotalPrice { get; protected set; }

        public virtual decimal ActualTotalPrice { get; protected set; }

        public virtual decimal RefundAmount { get; protected set; }

        [CanBeNull]
        public virtual string CustomerRemark { get; protected set; }

        [CanBeNull]
        public virtual string StaffRemark { get; protected set; }

        public virtual Guid? PaymentId { get; protected set; }

        public virtual DateTime? PaidTime { get; protected set; }

        public virtual DateTime? CompletionTime { get; protected set; }

        public virtual DateTime? CanceledTime { get; protected set; }

        [CanBeNull]
        public virtual string CancellationReason { get; protected set; }

        public virtual DateTime? ReducedInventoryAfterPlacingTime { get; protected set; }

        public virtual DateTime? ReducedInventoryAfterPaymentTime { get; protected set; }

        public virtual DateTime? ReducedInventoryAfterConsumingTime { get; protected set; }
        
        public virtual DateTime? PaymentExpiration { get; protected set; }
        
        public virtual List<OrderLine> OrderLines { get; protected set; }

        public virtual List<OrderExtraFee> OrderExtraFees { get; protected set; }

        public virtual Guid? StaffUserId { get; protected set; }

        public virtual Guid? OccupantId { get; protected set; }

        public virtual Guid? GraveId { get; protected set; }

        public virtual string ResourceType { get; protected set; }

        public virtual Guid? ResourceId { get; protected set; }

        public virtual string OrderType { get; protected set; }

        public Guid? RegionId { get; protected set; }

        protected Order()
        {
        }

        public Order(
            Guid id,
            Guid? tenantId,
            Guid storeId,
            Guid customerUserId,
            [NotNull] string currency,
            decimal productTotalPrice,
            decimal totalDiscount,
            decimal totalPrice,
            decimal actualTotalPrice,
            [CanBeNull] string customerRemark,
            [CanBeNull] string staffRemark,
            Guid? staffUserId = null,
            Guid? occupantId = null,
            Guid? graveId = null,
            Guid? regionId = null,
            string resourceType = null,
            Guid? resourceId = null,
            string orderType = null,
            DateTime? paymentExpiration = null
        ) : base(id)
        {
            TenantId = tenantId;
            StoreId = storeId;
            CustomerUserId = customerUserId;
            Currency = currency;
            ProductTotalPrice = productTotalPrice;
            TotalDiscount = totalDiscount;
            TotalPrice = totalPrice;
            ActualTotalPrice = actualTotalPrice;
            CustomerRemark = customerRemark;
            StaffRemark = staffRemark;
            PaymentExpiration = paymentExpiration;

            RefundAmount = 0;

            StaffUserId = staffUserId;
            OccupantId = occupantId;
            GraveId = graveId;
            RegionId = regionId;

            OrderStatus = OrderStatus.Pending;
            OrderLines = new List<OrderLine>();
            OrderExtraFees = new List<OrderExtraFee>();

            ResourceType = resourceType;

            ResourceId = resourceId;

            OrderType = orderType;
        }

        public void SetStaffRemark(string staffRemark)
        {
            StaffRemark = staffRemark;
        }

        public void SetCustomerRemark(string customerRemark)
        {
            CustomerRemark = customerRemark;
        }

        public void SetOrderNumber([NotNull] string orderNumber)
        {
            OrderNumber = orderNumber;
        }

        public void SetOrderLines(List<OrderLine> orderLines)
        {
            OrderLines = orderLines;
        }

        public void SetReducedInventoryAfterPlacingTime(DateTime? time)
        {
            ReducedInventoryAfterPlacingTime = time;
        }

        public void SetReducedInventoryAfterPaymentTime(DateTime? time)
        {
            ReducedInventoryAfterPaymentTime = time;
        }

        public void SetReducedInventoryAfterConsumingTime(DateTime? time)
        {
            ReducedInventoryAfterConsumingTime = time;
        }
        
        public void SetPaymentExpiration(DateTime? paymentExpiration)
        {
            PaymentExpiration = paymentExpiration;
        }

        public void SetPaymentId(Guid? paymentId)
        {
            AddLocalEvent(new OrderPaymentIdChangedEto(TenantId, this, PaymentId, paymentId));
            
            PaymentId = paymentId;
        }

        public void SetPaidTime(DateTime? paidTime)
        {
            PaidTime = paidTime;
        }

        public void SetCustomerUserId(Guid id)
        {
            CustomerUserId = id;
        }

        public void SetStaffUserId(Guid? id)
        {
            StaffUserId = id;
        }

        public void SetGraveId(Guid? id)
        {
            GraveId = id;
        }

        public void SetOccupantId(Guid? id)
        {
            OccupantId = id;
        }


        public void SetOrderStatus(OrderStatus orderStatus)
        {
            OrderStatus = orderStatus;
        }

        public void SetCompletionTime(DateTime? completionTime)
        {
            CompletionTime = completionTime;
        }

        public void SetCanceled(DateTime canceledTime, [CanBeNull] string cancellationReason)
        {
            CanceledTime = canceledTime;
            CancellationReason = cancellationReason;
        }

        public bool IsPaid()
        {
            return PaidTime.HasValue;
        }

        public void Refund(Guid orderLineId, int quantity, decimal amount)
        {
            if (amount <= decimal.Zero)
            {
                throw new InvalidRefundAmountException(amount);
            }

            var orderLine = OrderLines.Single(x => x.Id == orderLineId);

            if (orderLine.RefundedQuantity + quantity > orderLine.Quantity)
            {
                throw new InvalidRefundQuantityException(quantity);
            }

            orderLine.Refund(quantity, amount);

            RefundAmount += amount;
        }

        public bool IsInPayment()
        {
            return !(!PaymentId.HasValue || PaidTime.HasValue);
        }

        public void AddDiscount(Guid orderLineId, decimal expectedDiscountAmount)
        {
            if (OrderStatus != OrderStatus.Pending)
            {
                throw new OrderIsInWrongStageException(Id);
            }

            var orderLine = OrderLines.Single(x => x.Id == orderLineId);

            orderLine.AddDiscount(expectedDiscountAmount);

            TotalDiscount += expectedDiscountAmount;
            ActualTotalPrice -= expectedDiscountAmount;

            if (ActualTotalPrice < decimal.Zero)
            {
                throw new DiscountAmountOverflowException();
            }
        }

        public void AddOrderExtraFee(decimal extraFee, [NotNull] string extraFeeName, [CanBeNull] string extraFeeKey)
        {
            if (extraFee <= decimal.Zero)
            {
                throw new InvalidOrderExtraFeeException(extraFee);
            }

            var orderExtraFee = new OrderExtraFee(Id, extraFeeName, extraFeeKey, extraFee);

            if (OrderExtraFees.Any(x => x.EntityEquals(orderExtraFee)))
            {
                throw new DuplicateOrderExtraFeeException(extraFeeName, extraFeeKey);
            }

            OrderExtraFees.Add(orderExtraFee);

            TotalPrice += extraFee;
            ActualTotalPrice += extraFee;
        }

        public void UpdateOrderLineQuantity (Guid orderLineId, int changedQuantity)
        {
            var orderLine = OrderLines.FirstOrDefault(x => x.Id == orderLineId)
                             ?? throw new UserFriendlyException("订单商品不存在");

            if (changedQuantity == 0)
            {
                OrderLines.Remove(orderLine);

                if (!OrderLines.Any())
                {
                    throw new UserFriendlyException("订单需要至少一个商品");
                }
            }
            else
            {
                orderLine.UpdateQuantity(changedQuantity);
            }

            RecalculateTotalPrices();
        }



        private void RecalculateTotalPrices()
        {
            ProductTotalPrice = OrderLines.Sum(x => x.TotalPrice);
            TotalDiscount = OrderLines.Sum(x => x.TotalDiscount);

            if (ProductTotalPrice < decimal.Zero)
            {
                throw new UserFriendlyException("产品总价格不能小于零");
            }

            if (TotalDiscount < decimal.Zero)
            {
                throw new UserFriendlyException("总折扣不能小于零");
            }

            TotalPrice = ProductTotalPrice;

            TotalPrice += OrderExtraFees.Sum(x => x.Fee);

            ActualTotalPrice = TotalPrice - TotalDiscount;
        }
    }
}