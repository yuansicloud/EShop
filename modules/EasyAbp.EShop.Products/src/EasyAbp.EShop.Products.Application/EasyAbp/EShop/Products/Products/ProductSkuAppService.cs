using EasyAbp.EShop.Products.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSkuAppService : ReadOnlyAppService<ProductSku, ProductSkuDto, Guid, GetProductSkuListInput>, IProductSkuAppService
    {
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;

        private readonly IProductInventoryProvider _productInventoryProvider;
        private readonly IProductSkuRepository _repository;
        private readonly IProductManager _productManager;
        private readonly IDataFilter<IMultiTenant> _tenantFilter;

        public ProductSkuAppService(
            IProductInventoryProvider productInventoryProvider,
            IProductSkuRepository repository,
            IProductManager productManager, IDataFilter<IMultiTenant> tenantFilter) : base(repository)
        {
            _productInventoryProvider = productInventoryProvider;
            _repository = repository;
            _productManager = productManager;
            _tenantFilter = tenantFilter;
        }

        protected override async Task<IQueryable<ProductSku>> CreateFilteredQueryAsync(GetProductSkuListInput input)
        {
            using (_tenantFilter.Disable())
            {
                var query = await _repository.WithDetailsAsync();

                return query
                    .WhereIf(input.StoreId.HasValue, x => x.Product.StoreId == input.StoreId)
                    //.WhereIf(!input.ShowHidden, x => !x.Product.IsHidden)
                    //.WhereIf(input.IsPublished.HasValue, x => x.Product.IsPublished == input.IsPublished.Value)
                    .WhereIf(!input.Filter.IsNullOrEmpty(), x => x.Name.Contains(input.Filter))
                    .WhereIf(input.MinimumPrice.HasValue, x => x.Price >= input.MinimumPrice.Value)
                    .WhereIf(input.MaximumPrice.HasValue, x => x.Price <= input.MaximumPrice.Value);
            }
        }

        public override async Task<PagedResultDto<ProductSkuDto>> GetListAsync(GetProductSkuListInput input)
        {
            await CheckGetListPolicyAsync();

            // Todo: Products cache.
            var query = await CreateFilteredQueryAsync(input);

            //if (input.StoreId.HasValue)
            //{
            //    var isCurrentUserStoreAdmin =
            //        await AuthorizationService.IsMultiStoreGrantedAsync(input.StoreId,
            //            ProductsPermissions.Products.Default, ProductsPermissions.Products.CrossStore);

            //    if (input.ShowHidden && !isCurrentUserStoreAdmin)
            //    {
            //        throw new NotAllowedToGetProductListWithShowHiddenException();
            //    }

            //    if (!isCurrentUserStoreAdmin)
            //    {
            //        query = query.Where(x => x.Product.IsPublished);
            //    }
            //}
            //else
            //{
            //    if ((input.IsPublished.HasValue && input.IsPublished.Value) || input.ShowHidden)
            //    {
            //        await CheckPolicyAsync(ProductsPermissions.Products.CrossStore);
            //    }
            //}

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var productSkus = await AsyncExecuter.ToListAsync(query);

            var items = new List<ProductSkuDto>();

            foreach (var productSku in productSkus)
            {
                var productSkuDto = await MapToGetListOutputDtoAsync(productSku);

                await LoadDtoExtraDataAsync(productSku, productSkuDto);

                items.Add(productSkuDto);
            }

            return new PagedResultDto<ProductSkuDto>(totalCount, items);
        }

        protected virtual async Task<ProductSkuDto> LoadDtoInventoryDataAsync(ProductSku productSku, ProductSkuDto productSkuDto)
        {
            var inventoryData = await _productInventoryProvider.GetInventoryDataAsync(productSku.Product, productSku);

            productSkuDto.Sold = 0;

            productSkuDto.Inventory = inventoryData.Inventory;
            productSkuDto.Sold = inventoryData.Sold;
            productSkuDto.Sold += productSkuDto.Sold;

            return productSkuDto;
        }

        protected virtual async Task<ProductSkuDto> LoadDtoExtraDataAsync(ProductSku productSku, ProductSkuDto productSkuDto)
        {
            //await LoadDtoInventoryDataAsync(productSku, productSkuDto);
            //await LoadDtoPriceDataAsync(productSku, productSkuDto);

            return productSkuDto;
        }

        protected virtual async Task<ProductSkuDto> LoadDtoPriceDataAsync(ProductSku productSku, ProductSkuDto productSkuDto)
        {
            var priceDataModel = await _productManager.GetProductPriceAsync(productSku.Product, productSku);

            productSkuDto.Price = priceDataModel.Price;
            productSkuDto.DiscountedPrice = priceDataModel.DiscountedPrice;

            return productSkuDto;
        }

        protected virtual Task<ProductDto> LoadDtoAttributeOptionDisplayNamesAsync(ProductDto productDto)
        {
            foreach (var productSku in productDto.ProductSkus)
            {
                var attributeOptions = productDto.ProductAttributes
                    .SelectMany(a => a.ProductAttributeOptions);

                //TODO: Order
                productSku.AttributeOptionDisplayNames = productSku.AttributeOptionIds
                    .Select(i => attributeOptions.SingleOrDefault(o => o.Id == i)?.DisplayName)
                    .ToList();
            }

            return Task.FromResult(productDto);
        }
    }
}