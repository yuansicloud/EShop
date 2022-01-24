using EasyAbp.EShop.Orders.Authorization;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Orders.Orders
{
    [Authorize]
    public class OrderAppService : MultiStoreCrudAppService<Order, OrderDto, Guid, GetOrderListDto, CreateOrderDto>,
        IOrderAppService
    {
        protected override string GetPolicyName { get; set; } = OrdersPermissions.Orders.Manage;
        protected override string GetListPolicyName { get; set; } = OrdersPermissions.Orders.Manage;
        protected override string CrossStorePolicyName { get; set; } = OrdersPermissions.Orders.CrossStore;

        private readonly INewOrderGenerator _newOrderGenerator;
        private readonly IProductAppService _productAppService;
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _repository;

        public OrderAppService(
            INewOrderGenerator newOrderGenerator,
            IProductAppService productAppService,
            IOrderManager orderManager,
            IOrderRepository repository) : base(repository)
        {
            _newOrderGenerator = newOrderGenerator;
            _productAppService = productAppService;
            _orderManager = orderManager;
            _repository = repository;
        }

        protected override async Task<IQueryable<Order>> CreateFilteredQueryAsync(GetOrderListDto input)
        {
            return (await _repository.WithDetailsAsync())
                .WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId.Value)
                .WhereIf(input.CustomerUserId.HasValue, x => x.CustomerUserId == input.CustomerUserId.Value)
                .WhereIf(input.StaffUserId.HasValue, x => x.StaffUserId == input.StaffUserId.Value)
                .WhereIf(input.GraveId.HasValue, x => x.GraveId == input.GraveId.Value)
                .WhereIf(input.OccupantId.HasValue, x => x.OccupantId == input.OccupantId.Value)
                .WhereIf(input.MinActualTotalPrice.HasValue, x => x.ActualTotalPrice >= input.MinActualTotalPrice.Value)
                .WhereIf(input.MaxActualTotalPrice.HasValue, x => x.ActualTotalPrice <= input.MaxActualTotalPrice.Value)
                .WhereIf(input.MinCreationDate.HasValue, x => x.CreationTime.Date >= input.MinCreationDate.Value.Date)
                .WhereIf(input.MaxCreationDate.HasValue, x => x.CreationTime.Date <= input.MaxCreationDate.Value.Date)
                .WhereIf(input.MinCompletionDate.HasValue, x => x.CompletionTime.HasValue && x.CompletionTime.Value.Date >= input.MinCompletionDate.Value.Date)
                .WhereIf(input.MaxCompletionDate.HasValue, x => x.CompletionTime.HasValue && x.CompletionTime.Value.Date <= input.MaxCompletionDate.Value.Date)
                .WhereIf(input.MinPaidDate.HasValue, x => x.PaidTime.HasValue && x.PaidTime.Value.Date >= input.MinPaidDate.Value.Date)
                .WhereIf(input.MaxPaidDate.HasValue, x => x.PaidTime.HasValue && x.PaidTime.Value.Date <= input.MaxPaidDate.Value.Date)
                .WhereIf(input.OrderStatus.HasValue, x => x.OrderStatus == input.OrderStatus.Value)
                .WhereIf(!input.Filter.IsNullOrEmpty(), x => x.OrderNumber.Contains(input.Filter))
                .WhereIf(!input.Ids.IsNullOrEmpty(), x => input.Ids.Contains(x.Id))
                .WhereIf(input.RegionId.HasValue, x => x.RegionId == input.RegionId)
                .WhereIf(!input.OrderType.IsNullOrEmpty(), x => x.OrderType == input.OrderType);
        }

        [Authorize]
        public override async Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListDto input)
        {
            if (input.CustomerUserId != CurrentUser.GetId())
            {
                await CheckMultiStorePolicyAsync(input.StoreId, GetListPolicyName);
            }

            return await base.GetListAsync(input);
        }

        public override async Task<OrderDto> GetAsync(Guid id)
        {
            var order = await GetEntityByIdAsync(id);

            if (order.CustomerUserId != CurrentUser.GetId())
            {
                await CheckMultiStorePolicyAsync(order.StoreId, GetPolicyName);
            }

            return await MapToGetOutputDtoAsync(order);
        }

        public async Task<OrderDto> CreateByStaffAsync(CreateOrderDto input)
        {
            return null;
        }


        public async Task<List<OrderDto>> CreateInBulk(List<CreateOrderDto> input)
        {
            List<Order> orders = new();

            foreach (var createOrderDto in input)
            {
                var productDict = await GetProductDictionaryAsync(createOrderDto.OrderLines.Select(dto => dto.ProductId).ToList());

                await AuthorizationService.CheckAsync(
                    new OrderCreationResource
                    {
                        Input = createOrderDto,
                        ProductDictionary = productDict
                    },
                    new OrderOperationAuthorizationRequirement(OrderOperation.Creation)
                );

                var order = await _newOrderGenerator.GenerateAsync(createOrderDto.CustomerUserId ?? CurrentUser.GetId(), createOrderDto, productDict);

                await DiscountOrderAsync(order, productDict);

                orders.Add(order);
            }

            await Repository.InsertManyAsync(orders, autoSave: true);

            return await MapToGetListOutputDtosAsync(orders);
        }

        public override async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            // Todo: Check if the store is open.

            var productDict = await GetProductDictionaryAsync(input.OrderLines.Select(dto => dto.ProductId).ToList());

            //await AuthorizationService.CheckAsync(
            //    new OrderCreationResource
            //    {
            //        Input = input,
            //        ProductDictionary = productDict
            //    },
            //    new OrderOperationAuthorizationRequirement(OrderOperation.Creation)
            //);

            var order = await _newOrderGenerator.GenerateAsync(input.CustomerUserId ?? CurrentUser.GetId(), input, productDict);

            await DiscountOrderAsync(order, productDict);

            await Repository.InsertAsync(order, autoSave: true);

            return await MapToGetOutputDtoAsync(order);
        }

        protected virtual async Task DiscountOrderAsync(Order order, Dictionary<Guid, ProductDto> productDict)
        {
            foreach (var provider in ServiceProvider.GetServices<IOrderDiscountProvider>())
            {
                await provider.DiscountAsync(order, productDict);
            }
        }

        protected virtual async Task<Dictionary<Guid, ProductDto>> GetProductDictionaryAsync(
            IEnumerable<Guid> productIds)
        {
            var dict = new Dictionary<Guid, ProductDto>();

            foreach (var productId in productIds.Distinct().ToList())
            {
                dict.Add(productId, await _productAppService.GetAsync(productId));
            }

            return dict;
        }

        [RemoteService(false)]
        public override Task<OrderDto> UpdateAsync(Guid id, CreateOrderDto input)
        {
            throw new NotSupportedException();
        }

        [RemoteService(false)]
        public override Task DeleteAsync(Guid id)
        {
            throw new NotSupportedException();
        }

        public virtual async Task<OrderDto> GetByOrderNumberAsync(string orderNumber)
        {
            await CheckGetPolicyAsync();

            var order = await _repository.GetAsync(x => x.OrderNumber == orderNumber);

            if (order.CustomerUserId != CurrentUser.GetId())
            {
                await CheckMultiStorePolicyAsync(order.StoreId, OrdersPermissions.Orders.Manage);
            }

            return await MapToGetOutputDtoAsync(order);
        }

        [Authorize(OrdersPermissions.Orders.Complete)]
        public virtual async Task<OrderDto> CompleteAsync(Guid id)
        {
            var order = await GetEntityByIdAsync(id);

            if (order.CustomerUserId != CurrentUser.GetId())
            {
                await CheckMultiStorePolicyAsync(order.StoreId, OrdersPermissions.Orders.Manage);
            }

            order = await _orderManager.CompleteAsync(order);

            return await MapToGetOutputDtoAsync(order);
        }

        public virtual async Task<OrderDto> CancelAsync(Guid id, CancelOrderInput input)
        {
            var order = await GetEntityByIdAsync(id);

            await AuthorizationService.CheckAsync(
                order,
                new OrderOperationAuthorizationRequirement(OrderOperation.Cancellation)
            );

            order = await _orderManager.CancelAsync(order, input.CancellationReason);

            return await MapToGetOutputDtoAsync(order);
        }

        [AllowAnonymous]
        public async Task<OrderDto> CreateByImport(CreateOrderDto input)
        {
            // Todo: Check if the store is open.

            var productDict = await GetProductDictionaryAsync(input.OrderLines.Select(dto => dto.ProductId).ToList());

            //await AuthorizationService.CheckAsync(
            //    new OrderCreationResource
            //    {
            //        Input = input,
            //        ProductDictionary = productDict
            //    },
            //    new OrderOperationAuthorizationRequirement(OrderOperation.Creation)
            //);

            var order = await _newOrderGenerator.GenerateAsync(input.CustomerUserId ?? CurrentUser.GetId(), input, productDict);

            await DiscountOrderAsync(order, productDict);

            await Repository.InsertAsync(order, autoSave: true);

            return await MapToGetOutputDtoAsync(order);
        }
    }
}