using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.ProductStores;
using EasyAbp.EShop.Stores.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAppService : ApplicationService, IProductAppService
    {
        protected virtual string CreatePolicyName { get; set; } = ProductsPermissions.Products.Create;
        protected virtual string DeletePolicyName { get; set; } = ProductsPermissions.Products.Delete;
        protected virtual string UpdatePolicyName { get; set; } = ProductsPermissions.Products.Update;
        protected virtual string GetPolicyName { get; set; } = null;
        protected virtual string GetListPolicyName { get; set; } = null;

        private readonly IProductManager _productManager;
        private readonly EShopProductsOptions _options;
        private readonly IProductInventoryProvider _productInventoryProvider;
        private readonly IAttributeOptionIdsSerializer _attributeOptionIdsSerializer;
        private readonly IProductStoreRepository _productStoreRepository;
        private readonly IProductRepository _repository;

        public ProductAppService(
            IProductManager productManager,
            IOptions<EShopProductsOptions> options,
            IProductInventoryProvider productInventoryProvider,
            IAttributeOptionIdsSerializer attributeOptionIdsSerializer,
            IProductStoreRepository productStoreRepository,
            IProductRepository repository)
        {
            _productManager = productManager;
            _options = options.Value;
            _productInventoryProvider = productInventoryProvider;
            _attributeOptionIdsSerializer = attributeOptionIdsSerializer;
            _productStoreRepository = productStoreRepository;
            _repository = repository;
        }

        protected virtual IQueryable<ProductWithExtraData> CreateFilteredQuery(GetProductListDto input)
        {
            var query = _repository.WithDetails(input.StoreId, input.CategoryId);

            return input.ShowHidden ? query : query.Where(x => !x.Product.IsHidden);
        }

        protected virtual Task<Product> MapToEntityAsync(CreateUpdateProductDto createInput)
        {
            var product = ObjectMapper.Map<CreateUpdateProductDto, Product>(createInput);
            
            SetIdForGuids(product);
            
            return Task.FromResult(product);
        }
        
        protected virtual Task MapToEntityAsync(CreateUpdateProductDto updateInput, Product product)
        {
            ObjectMapper.Map(updateInput, product);

            return Task.CompletedTask;
        }
        
        protected virtual void SetIdForGuids(Product product)
        {
            if (product.Id == Guid.Empty)
            {
                EntityHelper.TrySetId(
                    product,
                    () => GuidGenerator.Create(),
                    true
                );
            }
        }

        public virtual async Task<ProductWithExtraDataDto> CreateAsync(CreateUpdateProductDto input)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(input.StoreId, CreatePolicyName,
                ProductsPermissions.Products.CrossStore);

            var product = await MapToEntityAsync(input);

            TryToSetTenantId(product);

            await UpdateProductAttributesAsync(product, input);

            await _productManager.CreateAsync(product, input.StoreId, input.CategoryIds);

            var productWithExtraData = await GetProductWithExtraDataByIdAsync(product.Id, input.StoreId);
            
            var dto = await MapToGetOutputDtoAsync(productWithExtraData);
            
            await LoadDtoExtraDataAsync(productWithExtraData.Product, dto.Product, input.StoreId);
            await LoadDtosProductGroupDisplayNameAsync(new[] {dto});
            
            return dto;
        }
        
        protected virtual void TryToSetTenantId(Product product)
        {
            var tenantId = CurrentTenant.Id;

            if (!tenantId.HasValue)
            {
                return;
            }

            var propertyInfo = product.GetType().GetProperty(nameof(IMultiTenant.TenantId));

            if (propertyInfo == null || propertyInfo.GetSetMethod(true) == null)
            {
                return;
            }

            propertyInfo.SetValue(product, tenantId);
        }

        public virtual async Task<ProductWithExtraDataDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(input.StoreId, UpdatePolicyName,
                ProductsPermissions.Products.CrossStore);

            await CheckStoreIsProductOwnerAsync(id, input.StoreId);

            var productWithExtraData = await GetProductWithExtraDataByIdAsync(id, input.StoreId);

            var product = productWithExtraData.Product;

            CheckProductIsNotStatic(product);

            await MapToEntityAsync(input, product);

            await UpdateProductAttributesAsync(product, input);

            await _productManager.UpdateAsync(product, input.CategoryIds);

            var dto = await MapToGetOutputDtoAsync(productWithExtraData);
            
            await LoadDtoExtraDataAsync(productWithExtraData.Product, dto.Product, input.StoreId);
            await LoadDtosProductGroupDisplayNameAsync(new[] {dto});

            return dto;
        }
        
        protected virtual async Task<List<ProductWithExtraDataDto>> MapToGetListOutputDtosAsync(List<ProductWithExtraData> entities)
        {
            var dtos = new List<ProductWithExtraDataDto>();

            foreach (var entity in entities)
            {
                dtos.Add(await MapToGetListOutputDtoAsync(entity));
            }

            return dtos;
        }
        
        protected virtual Task<ProductWithExtraDataDto> MapToGetListOutputDtoAsync(ProductWithExtraData productWithExtraData)
        {
            return MapToGetOutputDtoAsync(productWithExtraData);
        }
        
        protected virtual Task<ProductWithExtraDataDto> MapToGetOutputDtoAsync(ProductWithExtraData productWithExtraData)
        {
            return Task.FromResult(ObjectMapper.Map<ProductWithExtraData, ProductWithExtraDataDto>(productWithExtraData));
        }
        
        protected virtual async Task<ProductWithExtraData> GetProductWithExtraDataByIdAsync(Guid id, Guid storeId)
        {
            return await _repository.GetWithExtraDataAsync(id, storeId);
        }

        protected virtual async Task CheckStoreIsProductOwnerAsync(Guid productId, Guid storeId)
        {
            var productStore = await _productStoreRepository.GetAsync(productId, storeId);

            if (!productStore.IsOwner)
            {
                throw new StoreIsNotProductOwnerException(productId, storeId);
            }
        }

        protected virtual async Task UpdateProductAttributesAsync(Product product, CreateUpdateProductDto input)
        {
            var isProductSkusEmpty = product.ProductSkus.IsNullOrEmpty();

            var usedAttributeOptionIds = new HashSet<Guid>();

            foreach (var serializedAttributeOptionIds in product.ProductSkus.Select(sku =>
                sku.SerializedAttributeOptionIds))
            {
                foreach (var attributeOptionId in await _attributeOptionIdsSerializer.DeserializeAsync(
                    serializedAttributeOptionIds))
                {
                    usedAttributeOptionIds.Add(attributeOptionId);
                }
            }

            foreach (var attributeDto in input.ProductAttributes)
            {
                var attribute =
                    product.ProductAttributes.FirstOrDefault(a => a.DisplayName == attributeDto.DisplayName);

                if (attribute == null)
                {
                    if (!isProductSkusEmpty)
                    {
                        throw new ProductAttributesModificationFailedException();
                    }

                    attribute = new ProductAttribute(GuidGenerator.Create(),
                        attributeDto.DisplayName, attributeDto.Description);

                    product.ProductAttributes.Add(attribute);
                }

                foreach (var optionDto in attributeDto.ProductAttributeOptions)
                {
                    var option =
                        attribute.ProductAttributeOptions.FirstOrDefault(o => o.DisplayName == optionDto.DisplayName);

                    if (option == null)
                    {
                        option = new ProductAttributeOption(GuidGenerator.Create(),
                            optionDto.DisplayName, optionDto.Description);

                        attribute.ProductAttributeOptions.Add(option);
                    }
                }

                var removedOptionNames = attribute.ProductAttributeOptions.Select(o => o.DisplayName)
                    .Except(attributeDto.ProductAttributeOptions.Select(o => o.DisplayName)).ToList();

                if (!isProductSkusEmpty && removedOptionNames.Any() && usedAttributeOptionIds
                    .Intersect(attribute.ProductAttributeOptions
                        .Where(option => removedOptionNames.Contains(option.DisplayName))
                        .Select(option => option.Id)).Any())
                {
                    throw new ProductAttributeOptionsDeletionFailedException();
                }

                attribute.ProductAttributeOptions.RemoveAll(o => removedOptionNames.Contains(o.DisplayName));
            }

            var removedAttributeNames = product.ProductAttributes.Select(a => a.DisplayName)
                .Except(input.ProductAttributes.Select(a => a.DisplayName)).ToList();

            if (!isProductSkusEmpty && removedAttributeNames.Any())
            {
                throw new ProductAttributesModificationFailedException();
            }

            product.ProductAttributes.RemoveAll(a => removedAttributeNames.Contains(a.DisplayName));
        }

        public virtual async Task<ProductWithExtraDataDto> GetAsync(Guid id, Guid storeId)
        {
            var productWithExtraData = await GetProductWithExtraDataByIdAsync(id, storeId);

            if (!productWithExtraData.Product.IsPublished)
            {
                await CheckStoreIsProductOwnerAsync(productWithExtraData.Product.Id, storeId);
            }

            var dto = await MapToGetOutputDtoAsync(productWithExtraData);

            await LoadDtoExtraDataAsync(productWithExtraData.Product, dto.Product, storeId);
            await LoadDtosProductGroupDisplayNameAsync(new[] {dto});
            
            return dto;
        }

        protected virtual Task LoadDtosProductGroupDisplayNameAsync(IEnumerable<ProductWithExtraDataDto> dtos)
        {
            var dict = _options.Groups.GetConfigurationsDictionary();

            foreach (var dto in dtos)
            {
                dto.ProductGroupDisplayName = dict[dto.Product.ProductGroupName].DisplayName;
            }

            return Task.CompletedTask;
        }

        public virtual async Task<ProductWithExtraDataDto> GetByUniqueNameAsync(string uniqueName, Guid storeId)
        {
            var productWithExtraData = await _repository.GetByUniqueNameWithExtraDataAsync(uniqueName, storeId);

            if (!productWithExtraData.Product.IsPublished)
            {
                await CheckStoreIsProductOwnerAsync(productWithExtraData.Product.Id, storeId);
            }

            var dto = await MapToGetOutputDtoAsync(productWithExtraData);
            
            await LoadDtoExtraDataAsync(productWithExtraData.Product, dto.Product, storeId);
            await LoadDtosProductGroupDisplayNameAsync(new[] {dto});

            return dto;
        }

        public virtual async Task<PagedResultDto<ProductWithExtraDataDto>> GetListAsync(GetProductListDto input)
        {
            var isCurrentUserStoreAdmin =
                await AuthorizationService.IsMultiStoreGrantedAsync(input.StoreId,
                    ProductsPermissions.Products.Default, ProductsPermissions.Products.CrossStore);

            if (input.ShowHidden && !isCurrentUserStoreAdmin)
            {
                throw new NotAllowedToGetProductListWithShowHiddenException();
            }

            // Todo: Products cache.
            var query = CreateFilteredQuery(input);

            if (!isCurrentUserStoreAdmin)
            {
                query = query.Where(x => x.Product.IsPublished);
            }

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var productWithExtraDatas = await AsyncExecuter.ToListAsync(query);
            
            var dtos = new List<ProductWithExtraDataDto>();

            foreach (var productWithExtraData in productWithExtraDatas)
            {
                var productWithExtraDataDto = await MapToGetListOutputDtoAsync(productWithExtraData);

                await LoadDtoExtraDataAsync(productWithExtraData.Product, productWithExtraDataDto.Product, input.StoreId);
                
                dtos.Add(productWithExtraDataDto);
            }

            await LoadDtosProductGroupDisplayNameAsync(dtos);

            return new PagedResultDto<ProductWithExtraDataDto>(totalCount, dtos);
        }

        protected virtual IQueryable<ProductWithExtraData> ApplySorting(IQueryable<ProductWithExtraData> query, GetProductListDto input)
        {
            return !input.Sorting.IsNullOrWhiteSpace() ? query.OrderBy(input.Sorting) : ApplyDefaultSorting(query);
        }
        
        protected virtual IQueryable<ProductWithExtraData> ApplyDefaultSorting(IQueryable<ProductWithExtraData> query)
        {
            return query.OrderByDescending(e => e.Product.CreationTime);
        }
        
        protected virtual IQueryable<ProductWithExtraData> ApplyPaging(IQueryable<ProductWithExtraData> query, GetProductListDto input)
        {
            return query.PageBy(input);
        }
        
        protected virtual async Task<ProductDto> LoadDtoInventoryDataAsync(Product product, ProductDto productDto,
            Guid storeId)
        {
            var inventoryDataDict = await _productInventoryProvider.GetInventoryDataDictionaryAsync(product, storeId);

            productDto.Sold = 0;

            foreach (var productSkuDto in productDto.ProductSkus)
            {
                var inventoryData = inventoryDataDict[productSkuDto.Id];

                productSkuDto.Inventory = inventoryData.Inventory;
                productSkuDto.Sold = inventoryData.Sold;
                productDto.Sold += productSkuDto.Sold;
            }

            return productDto;
        }

        protected virtual async Task<ProductDto> LoadDtoExtraDataAsync(Product product, ProductDto productDto, Guid storeId)
        {
            await LoadDtoInventoryDataAsync(product, productDto, storeId);
            await LoadDtoPriceDataAsync(product, productDto, storeId);

            return productDto;
        }
        
        protected virtual async Task<ProductDto> LoadDtoPriceDataAsync(Product product, ProductDto productDto, Guid storeId)
        {
            foreach (var productSku in product.ProductSkus)
            {
                var productSkuDto = productDto.ProductSkus.First(x => x.Id == productSku.Id);

                var priceDataModel = await _productManager.GetProductPriceAsync(product, productSku, storeId);
                
                productSkuDto.Price = priceDataModel.Price;
                productSkuDto.DiscountedPrice = priceDataModel.DiscountedPrice;
            }

            if (productDto.ProductSkus.Count > 0)
            {
                productDto.MinimumPrice = productDto.ProductSkus.Min(sku => sku.Price);
                productDto.MaximumPrice = productDto.ProductSkus.Max(sku => sku.Price);
            }

            return productDto;
        }

        public async Task DeleteAsync(Guid id, Guid storeId)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(storeId, DeletePolicyName,
                ProductsPermissions.Products.CrossStore);

            var productWithExtraData = await GetProductWithExtraDataByIdAsync(id, storeId);

            CheckProductIsNotStatic(productWithExtraData.Product);

            await CheckStoreIsProductOwnerAsync(id, storeId);

            await _productManager.DeleteAsync(productWithExtraData.Product);
        }

        private static void CheckProductIsNotStatic(Product product)
        {
            if (product.IsStatic)
            {
                throw new StaticProductCannotBeModifiedException(product.Id);
            }
        }

        public async Task<ProductWithExtraDataDto> CreateSkuAsync(Guid productId, Guid storeId, CreateProductSkuDto input)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(storeId, UpdatePolicyName,
                ProductsPermissions.Products.CrossStore);

            await CheckStoreIsProductOwnerAsync(productId, storeId);

            var productWithExtraData = await GetProductWithExtraDataByIdAsync(productId, storeId);

            CheckProductIsNotStatic(productWithExtraData.Product);

            var sku = ObjectMapper.Map<CreateProductSkuDto, ProductSku>(input);

            EntityHelper.TrySetId(sku, GuidGenerator.Create);

            await _productManager.CreateSkuAsync(productWithExtraData.Product, sku);

            var dto = await MapToGetOutputDtoAsync(productWithExtraData);

            return dto;
        }

        public async Task<ProductWithExtraDataDto> UpdateSkuAsync(Guid productId, Guid productSkuId, Guid storeId,
            UpdateProductSkuDto input)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(storeId, UpdatePolicyName,
                ProductsPermissions.Products.CrossStore);

            await CheckStoreIsProductOwnerAsync(productId, storeId);

            var productWithExtraData = await GetProductWithExtraDataByIdAsync(productId, storeId);

            CheckProductIsNotStatic(productWithExtraData.Product);

            var sku = productWithExtraData.Product.ProductSkus.Single(x => x.Id == productSkuId);

            ObjectMapper.Map(input, sku);

            await _productManager.UpdateSkuAsync(productWithExtraData.Product, sku);

            var dto = await MapToGetOutputDtoAsync(productWithExtraData);

            return dto;
        }

        public async Task<ProductWithExtraDataDto> DeleteSkuAsync(Guid productId, Guid productSkuId, Guid storeId)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(storeId, UpdatePolicyName,
                ProductsPermissions.Products.CrossStore);

            await CheckStoreIsProductOwnerAsync(productId, storeId);

            var productWithExtraData = await GetProductWithExtraDataByIdAsync(productId, storeId);

            CheckProductIsNotStatic(productWithExtraData.Product);

            var sku = productWithExtraData.Product.ProductSkus.Single(x => x.Id == productSkuId);

            await _productManager.DeleteSkuAsync(productWithExtraData.Product, sku);

            var dto = await MapToGetOutputDtoAsync(productWithExtraData);

            return dto;
        }

        public virtual Task<ListResultDto<ProductGroupDto>> GetProductGroupListAsync()
        {
            var dict = _options.Groups.GetConfigurationsDictionary();

            return Task.FromResult(new ListResultDto<ProductGroupDto>(dict.Select(x =>
                new ProductGroupDto
                {
                    Name = x.Key,
                    DisplayName = x.Value.DisplayName,
                    Description = x.Value.Description
                }
            ).ToList()));
        }
    }
}