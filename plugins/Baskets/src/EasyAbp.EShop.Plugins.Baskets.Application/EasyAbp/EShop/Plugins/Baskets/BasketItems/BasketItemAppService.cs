using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;
using EasyAbp.EShop.Plugins.Baskets.Permissions;
using EasyAbp.EShop.Plugins.Baskets.ProductUpdates;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public class BasketItemAppService : CrudAppService<BasketItem, BasketItemDto, Guid, GetBasketItemListDto, CreateBasketItemDto, UpdateBasketItemDto>,
        IBasketItemAppService
    {
        protected override string GetPolicyName { get; set; } = BasketsPermissions.BasketItem.Default;
        protected override string GetListPolicyName { get; set; } = BasketsPermissions.BasketItem.Default;
        protected override string CreatePolicyName { get; set; } = BasketsPermissions.BasketItem.Create;
        protected override string UpdatePolicyName { get; set; } = BasketsPermissions.BasketItem.Update;
        protected override string DeletePolicyName { get; set; } = BasketsPermissions.BasketItem.Delete;

        private readonly IBasketItemRepository _repository;
        private readonly IProductUpdateRepository _productUpdateRepository;
        private readonly IProductAppService _productAppService;
        private readonly IProductSkuDescriptionProvider _productSkuDescriptionProvider;
        private readonly IOrderAppService _orderAppService;
        private readonly IDistributedEventBus _distributedEventBus;
        public BasketItemAppService(
            IBasketItemRepository repository,
            IProductUpdateRepository productUpdateRepository,
            IProductAppService productAppService,
            IOrderAppService orderAppService,
            IDistributedEventBus distributedEventBus,
            IProductSkuDescriptionProvider productSkuDescriptionProvider) : base(repository)
        {
            _repository = repository;
            _productUpdateRepository = productUpdateRepository;
            _productAppService = productAppService;
            _productSkuDescriptionProvider = productSkuDescriptionProvider;
            _orderAppService = orderAppService;
            _distributedEventBus = distributedEventBus;
        }

        public override async Task<BasketItemDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            var item = await GetEntityByIdAsync(id);

            if (item.IdentifierId != CurrentUser.GetId() && !await IsCurrentUserManagerAsync())
            {
                throw new AbpAuthorizationException();
            }

            var productUpdate = await _productUpdateRepository.FindAsync(x => x.ProductSkuId == item.ProductSkuId);

            if (productUpdate != null)
            {
                var itemUpdateTime = item.LastModificationTime ?? item.CreationTime;
                var productUpdateTime = productUpdate.LastModificationTime ?? productUpdate.CreationTime;

                if (itemUpdateTime < productUpdateTime)
                {
                    var productDto = await _productAppService.GetAsync(item.ProductId);

                    await UpdateProductDataAsync(item.Quantity, item, productDto);

                    await _repository.UpdateAsync(item, true);
                }
            }

            return await MapToGetOutputDtoAsync(item);
        }

        public override async Task<PagedResultDto<BasketItemDto>> GetListAsync(GetBasketItemListDto input)
        {
            await CheckGetListPolicyAsync();

            if (input.IdentifierId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(BasketsPermissions.BasketItem.Manage);
            }

            var query = await CreateFilteredQueryAsync(input);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var items = await AsyncExecuter.ToListAsync(query);

            var productSkuIds = items.Select(item => item.ProductSkuId).ToList();

            var skuIdUpdateTimeDict =
                (await _productUpdateRepository.GetListByProductSkuIdsAsync(productSkuIds)).ToDictionary(
                    x => x.ProductSkuId, x => x.LastModificationTime ?? x.CreationTime);

            var productDtoDict = new Dictionary<Guid, ProductDto>();

            foreach (var item in items)
            {
                if (!skuIdUpdateTimeDict.ContainsKey(item.ProductSkuId))
                {
                    continue;
                }

                var itemUpdateTime = item.LastModificationTime ?? item.CreationTime;
                var productUpdateTime = skuIdUpdateTimeDict[item.ProductSkuId];

                if (itemUpdateTime >= productUpdateTime)
                {
                    continue;
                }

                if (!productDtoDict.ContainsKey(item.ProductId))
                {
                    // Todo: deleted product cause errors
                    productDtoDict[item.ProductId] = await _productAppService.GetAsync(item.ProductId);
                }

                await UpdateProductDataAsync(item.Quantity, item, productDtoDict[item.ProductId]);

                await _repository.UpdateAsync(item);
            }

            return new PagedResultDto<BasketItemDto>(
                totalCount,
                await MapToGetListOutputDtosAsync(items.OrderBy(x => !x.IsStatic).ToList())
            );
        }

        protected virtual async Task UpdateProductDataAsync(int quantity, BasketItem item, ProductDto productDto, decimal? unitPrice = null, decimal totalDiscount = 0)
        {
            item.SetIsInvalid(false);

            var productSkuDto = productDto.FindSkuById(item.ProductSkuId);

            if (productSkuDto == null)
            {
                item.SetIsInvalid(true);

                return;
            }

            if (productSkuDto.IsFixedPrice && unitPrice.HasValue && unitPrice.Value != productSkuDto.DiscountedPrice)
            {
                throw new UserFriendlyException("非可调价商品！");
            }

            if (productDto.InventoryStrategy != InventoryStrategy.NoNeed && quantity > productSkuDto.Inventory)
            {
                item.SetIsInvalid(true);
            }

            item.UpdateProductData(quantity, new ProductDataModel
            {
                MediaResources = productSkuDto.MediaResources ?? productDto.MediaResources,
                ProductUniqueName = productDto.UniqueName,
                ProductDisplayName = productDto.DisplayName,
                SkuName = productSkuDto.Name,
                SkuDescription = await _productSkuDescriptionProvider.GenerateAsync(productDto, productSkuDto),
                Currency = productSkuDto.Currency,
                UnitPrice = unitPrice ?? productSkuDto.DiscountedPrice,
                TotalPrice = (unitPrice ?? productSkuDto.DiscountedPrice) * quantity,
                TotalDiscount = totalDiscount, //(productSkuDto.Price - productSkuDto.DiscountedPrice) * item.Quantity,
                Inventory = productSkuDto.Inventory,
                IsFixedPrice = productSkuDto.IsFixedPrice
            });

            if (!productDto.IsPublished)
            {
                item.SetIsInvalid(true);
            }

        }

        protected override async Task<IQueryable<BasketItem>> CreateFilteredQueryAsync(GetBasketItemListDto input)
        {
            var userId = input.IdentifierId ?? CurrentUser.GetId();

            return ReadOnlyRepository.Where(item => item.IdentifierId == userId && item.BasketName == input.BasketName);
        }

        public async Task<List<BasketItemDto>> CreateInBulkAsync(List<CreateBasketItemDto> input)
        {
            List<BasketItemDto> basketItems = new();

            foreach(var item in input)
            {
                basketItems.Add(await CreateAsync(item));
            }

            return basketItems;
        }

        public override async Task<BasketItemDto> CreateAsync(CreateBasketItemDto input)
        {
            await CheckCreatePolicyAsync();

            var identifierId = input.IdentifierId ?? CurrentUser.GetId();

            if (identifierId != CurrentUser.GetId() && !await IsCurrentUserManagerAsync())
            {
                throw new AbpAuthorizationException();
            }

            var productDto = await _productAppService.GetAsync(input.ProductId);

            var item = await _repository.FindAsync(x =>
                x.IdentifierId == identifierId && x.BasketName == input.BasketName && x.ProductSkuId == input.ProductSkuId);

            if (item != null)
            {
                await UpdateProductDataAsync(input.Quantity + item.Quantity, item, productDto);

                await Repository.UpdateAsync(item, autoSave: true);

                return await MapToGetOutputDtoAsync(item);
            }

            var productSkuDto = productDto.FindSkuById(input.ProductSkuId);

            if (productSkuDto == null)
            {
                throw new ProductSkuNotFoundException(input.ProductId, input.ProductSkuId);
            }

            item = new BasketItem(GuidGenerator.Create(), CurrentTenant.Id, input.BasketName, identifierId,
                productDto.StoreId, input.ProductId, input.ProductSkuId, false);

            await UpdateProductDataAsync(input.Quantity, item, productDto);

            await Repository.InsertAsync(item, autoSave: true);

            return await MapToGetOutputDtoAsync(item);
        }

        public override async Task<BasketItemDto> UpdateAsync(Guid id, UpdateBasketItemDto input)
        {
            await CheckUpdatePolicyAsync();

            var item = await GetEntityByIdAsync(id);

            if (item.IdentifierId != CurrentUser.GetId() && !await IsCurrentUserManagerAsync())
            {
                throw new AbpAuthorizationException();
            }

            var productDto = await _productAppService.GetAsync(item.ProductId);

            await UpdateProductDataAsync(input.Quantity, item, productDto, input.UnitPrice, input.TotalDiscount);

            await Repository.UpdateAsync(item, autoSave: true);

            return await MapToGetOutputDtoAsync(item);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await CheckDeletePolicyAsync();

            var item = await _repository.GetAsync(id);

            if (item.IdentifierId != CurrentUser.GetId() && !await IsCurrentUserManagerAsync())
            {
                throw new AbpAuthorizationException();
            }

            await _repository.DeleteAsync(item, true);
        }

        public virtual async Task DeleteInBulkAsync(IEnumerable<Guid> ids)
        {
            await CheckDeletePolicyAsync();

            var isCurrentUserManager = await IsCurrentUserManagerAsync();

            foreach (var id in ids)
            {
                var item = await GetEntityByIdAsync(id);

                if (item.IdentifierId != CurrentUser.GetId() && !isCurrentUserManager)
                {
                    throw new AbpAuthorizationException();
                }

                await _repository.DeleteAsync(item);
            }
        }

        public virtual async Task CreateOrderFromBasket(CreateOrderFromBasketInput input)
        {
            var basketItems = _repository
                .Where(x => x.BasketName == input.BasketName && x.IdentifierId == (input.IdentifierId ?? CurrentUser.GetId()))
                .ToList();

            if (basketItems.Where(i => i.IsInvalid).Count() > 0)
            {
                throw new UserFriendlyException("购物车存在失效商品, 请先删除!");
            }

            var orderGroups = basketItems.GroupBy(x => x.StoreId);

            List<CreateOrderDto> createOrderDtos = new();

            foreach (var orderGroup in orderGroups)
            {
                var orderLines = orderGroup.Select(i => new CreateOrderLineDto
                {
                    ProductId = i.ProductId,
                    ProductSkuId = i.ProductSkuId,
                    Quantity = i.Quantity,
                    TotalDiscount = i.TotalDiscount,
                    UnitPrice = i.IsFixedPrice ? null : i.UnitPrice
                });

                createOrderDtos.Add(new CreateOrderDto
                {
                    CustomerRemark = input.CustomerRemark,
                    StaffRemark = input.StaffRemark,
                    CustomerUserId = input.CustomerUserId ?? CurrentUser.GetId(),
                    GraveId = input.GraveId,
                    OccupantId = input.OccupantId,
                    StaffUserId = input.StaffUserId,
                    StoreId = orderGroup.Key,
                    OrderLines = orderLines.ToList()

                });

            }

            var orders = await _orderAppService.CreateInBulk(createOrderDtos);

            foreach (var order in orders)
            {
                await _distributedEventBus.PublishAsync(new OrderFromBasketCreatedEto(order.Id, input.BasketName, input.IdentifierId ?? CurrentUser.GetId(), CurrentTenant.Id));
            }

            await _repository.DeleteManyAsync(basketItems);
        }

        protected virtual async Task<bool> IsCurrentUserManagerAsync()
        {
            return await AuthorizationService.IsGrantedAsync(BasketsPermissions.BasketItem.Manage);
        }
    }
}