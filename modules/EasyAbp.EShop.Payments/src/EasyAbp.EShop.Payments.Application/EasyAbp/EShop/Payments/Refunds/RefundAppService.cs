using EasyAbp.EShop.Payments.Authorization;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using EasyAbp.EShop.Stores.Permissions;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Refunds;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Json;
using Volo.Abp.Users;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Payments.Refunds
{
    [Authorize]
    public class RefundAppService : ReadOnlyAppService<Refund, RefundDto, Guid, GetRefundListDto>,
        IRefundAppService
    {
        protected override string GetPolicyName { get; set; } = PaymentsPermissions.Refunds.Manage;
        protected override string GetListPolicyName { get; set; } = PaymentsPermissions.Refunds.Manage;

        private readonly IOrderAppService _orderAppService;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IRefundRepository _repository;

        public RefundAppService(
            IOrderAppService orderAppService,
            IDistributedEventBus distributedEventBus,
            IPaymentRepository paymentRepository,
            IJsonSerializer jsonSerializer,
            IRefundRepository repository) : base(repository)
        {
            _orderAppService = orderAppService;
            _distributedEventBus = distributedEventBus;
            _paymentRepository = paymentRepository;
            _jsonSerializer = jsonSerializer;
            _repository = repository;
        }

        // Todo: should a store owner user see orders of other stores in the same payment/refund?
        public override async Task<RefundDto> GetAsync(Guid id)
        {
            var refund = await base.GetAsync(id);

            var payment = await _paymentRepository.GetAsync(refund.PaymentId);

            if (payment.UserId != CurrentUser.GetId())
            {
                await CheckPolicyAsync(GetPolicyName);
            }

            return refund;
        }

        public async Task<RefundDto> FindByPaymentIdAsync(Guid paymentId)
        {
            var refund = await _repository.SingleOrDefaultAsync(x => x.PaymentId == paymentId);

            var payment = await _paymentRepository.GetAsync(refund.PaymentId);

            if (payment.UserId != CurrentUser.GetId())
            {
                await CheckPolicyAsync(GetPolicyName);
            }

            return await MapToGetOutputDtoAsync(refund);
        }

        protected override async Task<IQueryable<Refund>> CreateFilteredQueryAsync(GetRefundListDto input)
        {
            var query = input.UserId.HasValue
                ? await _repository.GetQueryableByUserIdAsync(input.UserId.Value)
                : await _repository.WithDetailsAsync();

            return query.WhereIf(!input.Filter.IsNullOrEmpty(), x => x.DisplayReason.Contains(input.Filter) || x.CustomerRemark.Contains(input.Filter) || x.StaffRemark.Contains(input.Filter))
                        .WhereIf(!input.RefundPaymentMethod.IsNullOrEmpty(), x => x.RefundPaymentMethod == input.RefundPaymentMethod)
                        .WhereIf(!input.ExternalTradingCode.IsNullOrEmpty(), x => x.ExternalTradingCode.Contains(input.ExternalTradingCode))
                        .WhereIf(input.IsCompleted.HasValue, x => x.CompletedTime.HasValue)
                        .WhereIf(input.IsCancelled.HasValue, x => x.CanceledTime.HasValue)
                        .WhereIf(input.MinCompletedTime.HasValue, x => x.CompletedTime.HasValue && x.CompletedTime >= input.MinCompletedTime.Value)
                        .WhereIf(input.MaxCompletedTime.HasValue, x => x.CompletedTime.HasValue && x.CompletedTime <= input.MaxCompletedTime.Value)
                        .WhereIf(input.MinCancelledTime.HasValue, x => x.CanceledTime.HasValue && x.CanceledTime >= input.MinCancelledTime.Value)
                        .WhereIf(input.MaxCancelledTime.HasValue, x => x.CanceledTime.HasValue && x.CanceledTime <= input.MaxCancelledTime.Value)
                        .WhereIf(input.MinCreationTime.HasValue, x => x.CreationTime >= input.MinCreationTime.Value)
                        .WhereIf(input.MaxCreationTime.HasValue, x => x.CreationTime <= input.MaxCreationTime.Value)
                        .WhereIf(input.MinRefundAmount.HasValue, x => x.RefundAmount >= input.MinRefundAmount.Value)
                        .WhereIf(input.MaxRefundAmount.HasValue, x => x.RefundAmount <= input.MaxRefundAmount.Value);
        }

        // Todo: should a store owner user see orders of other stores in the same payment/refund?
        [Authorize]
        public override async Task<PagedResultDto<RefundDto>> GetListAsync(GetRefundListDto input)
        {
            if (input.UserId != CurrentUser.GetId())
            {
                await CheckPolicyAsync(GetListPolicyName);
            }

            return await base.GetListAsync(input);
        }

        public virtual async Task CreateAsync(CreateEShopRefundInput input)
        {
            await AuthorizationService.CheckAsync(PaymentsPermissions.Refunds.Manage);

            var payment = await _paymentRepository.GetAsync(input.PaymentId);

            var createRefundInput = new CreateRefundInput
            {
                PaymentId = input.PaymentId,
                DisplayReason = input.DisplayReason,
                CustomerRemark = input.CustomerRemark,
                StaffRemark = input.StaffRemark
            };

            if (await _repository.FindAsync(r => r.PaymentId == input.PaymentId) != null)
            {
                throw new UserFriendlyException("该付款单存在正在进行中的退款单");
            }

            foreach (var refundItem in input.RefundItems)
            {
                var order = await _orderAppService.GetAsync(refundItem.OrderId);

                var paymentItem = payment.PaymentItems.SingleOrDefault(x => x.ItemKey == refundItem.OrderId.ToString());

                if (order.PaymentId != input.PaymentId || paymentItem == null)
                {
                    throw new OrderIsNotInSpecifiedPaymentException(order.Id, payment.Id);
                }

                // Todo: Check if current user is an admin of the store.

                foreach (var orderLineRefundInfoModel in refundItem.OrderLines)
                {
                    var orderLine = order.OrderLines.Single(x => x.Id == orderLineRefundInfoModel.OrderLineId);

                    if (orderLine.RefundedQuantity + orderLineRefundInfoModel.Quantity > orderLine.Quantity)
                    {
                        throw new InvalidRefundQuantityException(orderLineRefundInfoModel.Quantity);
                    }
                }

                createRefundInput.RefundItems.Add(new CreateRefundItemInput
                {
                    PaymentItemId = paymentItem.Id,
                    RefundAmount = refundItem.OrderLines.Sum(x => x.TotalAmount),
                    CustomerRemark = refundItem.CustomerRemark,
                    StaffRemark = refundItem.StaffRemark,
                    ExtraProperties = new ExtraPropertyDictionary
                    {
                        {"StoreId", order.StoreId.ToString()},
                        {"OrderId", order.Id.ToString()},
                        {"OrderLines", _jsonSerializer.Serialize(refundItem.OrderLines)}
                    }
                });
            }

            if (createRefundInput.RefundItems.Sum(x => x.RefundAmount) <= 0)
            {
                throw new UserFriendlyException("退款金额不能小于0");
            }

            await _distributedEventBus.PublishAsync(new RefundPaymentEto(CurrentTenant.Id, createRefundInput));
        }
    }
}