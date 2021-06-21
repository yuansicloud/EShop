using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Authorization;
using EasyAbp.EShop.Payments.Payments.Dtos;
using EasyAbp.PaymentService.Payments;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Payments.Payments
{
    [Authorize]
    public class PaymentAppService : ReadOnlyAppService<Payment, PaymentDto, Guid, GetPaymentListDto>,
        IPaymentAppService
    {
        protected override string GetPolicyName { get; set; } = PaymentsPermissions.Payments.Manage;
        protected override string GetListPolicyName { get; set; } = PaymentsPermissions.Payments.Manage;

        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IOrderAppService _orderAppService;
        private readonly IPaymentRepository _repository;

        public PaymentAppService(
            IDistributedEventBus distributedEventBus,
            IOrderAppService orderAppService,
            IPaymentRepository repository) : base(repository)
        {
            _distributedEventBus = distributedEventBus;
            _orderAppService = orderAppService;
            _repository = repository;
        }

        // Todo: should a store owner user see orders of other stores in the same payment/refund?
        public override async Task<PaymentDto> GetAsync(Guid id)
        {
            var payment = await base.GetAsync(id);

            if (payment.UserId != CurrentUser.GetId())
            {
                await CheckPolicyAsync(GetPolicyName);
            }

            return payment;
        }

        protected override async Task<IQueryable<Payment>> CreateFilteredQueryAsync(GetPaymentListDto input)
        {
            var query = await _repository.WithDetailsAsync();

            if (input.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == input.UserId.Value);
            }

            return query.WhereIf(!input.PayeeAccount.IsNullOrEmpty(), x => x.PayeeAccount.Contains(input.PayeeAccount))
                        .WhereIf(!input.PaymentMethod.IsNullOrEmpty(), x => x.PaymentMethod == input.PaymentMethod)
                        .WhereIf(!input.ExternalTradingCode.IsNullOrEmpty(), x => x.ExternalTradingCode.Contains(input.ExternalTradingCode))
                        .WhereIf(input.IsCompleted.HasValue, x => x.CompletionTime.HasValue)
                        .WhereIf(input.IsCancelled.HasValue, x => x.CanceledTime.HasValue)
                        .WhereIf(input.MinCompletedTime.HasValue, x => x.CompletionTime.HasValue && x.CompletionTime >= input.MinCompletedTime.Value)
                        .WhereIf(input.MaxCompletedTime.HasValue, x => x.CompletionTime.HasValue && x.CompletionTime <= input.MaxCompletedTime.Value)
                        .WhereIf(input.MinCancelledTime.HasValue, x => x.CanceledTime.HasValue && x.CanceledTime >= input.MinCancelledTime.Value)
                        .WhereIf(input.MaxCancelledTime.HasValue, x => x.CanceledTime.HasValue && x.CanceledTime <= input.MaxCancelledTime.Value)
                        .WhereIf(input.MinCreationTime.HasValue, x => x.CreationTime >= input.MinCreationTime.Value)
                        .WhereIf(input.MaxCreationTime.HasValue, x =>  x.CreationTime <= input.MaxCreationTime.Value)
                        .WhereIf(input.MinPaymentAmount.HasValue, x => x.ActualPaymentAmount >= input.MinPaymentAmount.Value)
                        .WhereIf(input.MaxPaymentAmount.HasValue, x => x.ActualPaymentAmount <= input.MaxPaymentAmount.Value);
        }

        // Todo: should a store owner user see orders of other stores in the same payment/refund?
        [Authorize]
        public override async Task<PagedResultDto<PaymentDto>> GetListAsync(GetPaymentListDto input)
        {
            if (input.UserId != CurrentUser.GetId())
            {
                await CheckPolicyAsync(GetListPolicyName);
            }

            return await base.GetListAsync(input);
        }

        [Authorize(PaymentsPermissions.Payments.Create)]
        public virtual async Task CreateAsync(CreatePaymentDto input)
        {
            // Todo: should avoid duplicate creations. (concurrent lock)

            var orders = new List<OrderDto>();

            foreach (var orderId in input.OrderIds)
            {
                orders.Add(await _orderAppService.GetAsync(orderId));
            }

            if (orders.Select(s => s.CustomerUserId).Distinct().Count() > 1)
            {
                throw new UserFriendlyException("不能同时支付多个客户的订单");
            }

            await AuthorizationService.CheckAsync(
                new PaymentCreationResource
                {
                    Input = input,
                    Orders = orders
                },
                new PaymentOperationAuthorizationRequirement(PaymentOperation.Creation)
            );

            var createPaymentEto = new CreatePaymentEto(
                CurrentTenant.Id,
                orders.FirstOrDefault()?.CustomerUserId ?? CurrentUser.GetId(),
                input.PaymentMethod,
                orders.First().Currency,
                orders.Select(order => new CreatePaymentItemEto
                {
                    ItemType = PaymentsConsts.PaymentItemType,
                    ItemKey = order.Id.ToString(),
                    OriginalPaymentAmount = order.ActualTotalPrice,
                    ExtraProperties = new ExtraPropertyDictionary {{"StoreId", order.StoreId.ToString()}}
                }).ToList()
            );

            await _distributedEventBus.PublishAsync(createPaymentEto);
        }
    }
}