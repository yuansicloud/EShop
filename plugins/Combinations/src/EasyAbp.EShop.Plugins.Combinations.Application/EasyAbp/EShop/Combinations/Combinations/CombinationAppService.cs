using EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos;
using EasyAbp.EShop.Plugins.Combinations.Permissions;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public class CombinationAppService : CrudAppService<Combination, CombinationDto, Guid, GetCombinationListInput, CreateCombinationDto, UpdateCombinationDto>,
        ICombinationAppService
    {
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;
        protected override string CreatePolicyName { get; set; } = CombinationsPermissions.Combinations.Manage;
        protected override string UpdatePolicyName { get; set; } = CombinationsPermissions.Combinations.Manage;
        protected override string DeletePolicyName { get; set; } = CombinationsPermissions.Combinations.Manage;

        private readonly ICombinationRepository _repository;

        private readonly ICombinationManager _combinationManager;

        private readonly IProductAppService _productAppService;

        private readonly IProductSkuDescriptionProvider _productSkuDescriptionProvider;

        public CombinationAppService(
            ICombinationRepository repository,
            ICombinationManager combinationManager,
            IProductAppService productAppService,
            IProductSkuDescriptionProvider productSkuDescriptionProvider) : base(repository)
        {
            _repository = repository;
            _combinationManager = combinationManager;
            _productAppService = productAppService;
            _productSkuDescriptionProvider = productSkuDescriptionProvider;
        }

        public override async Task<CombinationDto> CreateAsync(CreateCombinationDto input)
        {
            var combination = MapToEntity(input);

            TryToSetTenantId(combination);

            await _combinationManager.CreateAsync(combination);

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }

        public override async Task<CombinationDto> UpdateAsync(Guid id, UpdateCombinationDto input)
        {
            var combination = await GetEntityByIdAsync(id);

            MapToEntity(input, combination);

            await _combinationManager.UpdateAsync(combination);

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }

        public override async Task<CombinationDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            var combination = await GetEntityByIdAsync(id);

            if (!combination.IsPublished)
            {
                await CheckPolicyAsync(CombinationsPermissions.Combinations.Manage);
            }

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }

        public async Task<CombinationDto> FindAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            var combination = await _repository.FindAsync(id);

            if (combination is null)
            {
                return null;
            }

            if (!combination.IsPublished)
            {
                await CheckPolicyAsync(CombinationsPermissions.Combinations.Manage);
            }

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }

        public async Task<CombinationDto> CreateCombinationItemAsync(Guid id, CreateCombinationItemDto input)
        {
            await CheckUpdatePolicyAsync();

            var combination = await GetEntityByIdAsync(id);

            var productDto = await _productAppService.GetAsync(input.ProductId);

            var item = combination.CombinationItems.Find(x => x.ProductSkuId == input.ProductSkuId);

            if (item != null)
            {
                throw new UserFriendlyException("�ײ����Ѿ����ڸ���ƷSKU");
            }

            var productSkuDto = productDto.FindSkuById(input.ProductSkuId);

            if (productSkuDto == null)
            {
                throw new UserFriendlyException("��Ʒ������");
            }

            item = new CombinationItem(GuidGenerator.Create(), CurrentTenant.Id,
                productDto.StoreId, input.ProductId, input.ProductSkuId);

            await UpdateProductDataAsync(input.Quantity, item, productDto, input.UnitPrice, input.TotalDiscount);

            await _combinationManager.CreateCombinationItemAsync(combination, item);

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }

        public async Task<CombinationDto> UpdateCombinationItemAsync(Guid id, Guid combinationItemId, UpdateCombinationItemDto input)
        {
            await CheckUpdatePolicyAsync();

            var combination = await GetEntityByIdAsync(id);

            var item = combination.CombinationItems.Find(x => x.Id == combinationItemId);

            var productDto = await _productAppService.GetAsync(item.ProductId);

            await UpdateProductDataAsync(input.Quantity, item, productDto, input.UnitPrice, input.TotalDiscount);

            await _combinationManager.UpdateCombinationItemAsync(combination, item);

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }

        public async Task<CombinationDto> DeleteCombinationItemAsync(Guid id, Guid combinationItemId)
        {
            var combination = await GetEntityByIdAsync(id);

            await CheckUpdatePolicyAsync();

            var sku = combination.CombinationItems.Single(x => x.Id == combinationItemId);

            await _combinationManager.DeleteCombinationItemAsync(combination, sku);

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }

        public virtual async Task<CombinationDto> ChangeCombinationPublished(Guid id, ChangeCombinationPublishedDto input)
        {
            var combination = await GetEntityByIdAsync(id);

            await CheckUpdatePolicyAsync();

            combination.TogglePublished(input.IsPublished);

            await _repository.UpdateAsync(combination, autoSave: true);

            var dto = await MapToGetOutputDtoAsync(combination);

            return dto;
        }
        protected override async Task<IQueryable<Combination>> CreateFilteredQueryAsync(GetCombinationListInput input)
        {
            var query = await _repository.WithDetailsAsync();

            var isManager = await AuthorizationService.IsGrantedAsync(CombinationsPermissions.Combinations.Manage);

            if (!isManager)
            {
                query = query.Where(x => x.IsPublished);
            }

            return query
                .WhereIf(!input.Filter.IsNullOrEmpty(), x => x.DisplayName.Contains(input.Filter))
                .WhereIf(input.MinimumPrice.HasValue, x => x.CombinationItems.Sum(x => x.UnitPrice * x.Quantity - x.TotalDiscount) >= input.MinimumPrice.Value)
                .WhereIf(input.MaximumPrice.HasValue, x => x.CombinationItems.Sum(x => x.UnitPrice * x.Quantity - x.TotalDiscount) <= input.MaximumPrice.Value);
        }

        protected virtual async Task UpdateProductDataAsync(int quantity, CombinationItem item, ProductDto productDto, decimal? unitPrice = null, decimal totalDiscount = 0)
        {
            //item.SetIsInvalid(false);

            var productSkuDto = productDto.FindSkuById(item.ProductSkuId);

            if (productSkuDto == null)
            {
                //item.SetIsInvalid(true);

                return;
            }

            if (productSkuDto.IsFixedPrice && unitPrice.HasValue && unitPrice.Value != productSkuDto.DiscountedPrice)
            {
                throw new UserFriendlyException("�ǿɵ�����Ʒ��");
            }

            //if (productDto.InventoryStrategy != InventoryStrategy.NoNeed && quantity > productSkuDto.Inventory)
            //{
            //    item.SetIsInvalid(true);
            //}

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

            //if (!productDto.IsPublished)
            //{
            //    item.SetIsInvalid(true);
            //}

        }
    }
}
