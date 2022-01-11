using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public class CreateUpdateStaticBasketItemEventHandler : IDistributedEventHandler<CreateUpdateStaticBasketItemEto>, ITransientDependency
    {

        private readonly IGuidGenerator _guidGenerator;
        private readonly IBasketItemRepository _repository;
        private readonly IProductAppService _productAppService;
        private readonly IProductSkuDescriptionProvider _productSkuDescriptionProvider;
        public CreateUpdateStaticBasketItemEventHandler(
            IProductAppService productAppService,
            IProductSkuDescriptionProvider productSkuDescriptionProvider,
            IGuidGenerator guidGenerator,
            IBasketItemRepository repository)
        {
            _repository = repository;
            _productAppService = productAppService;
            _productSkuDescriptionProvider = productSkuDescriptionProvider;
            _guidGenerator = guidGenerator;
        }

        public async Task HandleEventAsync(CreateUpdateStaticBasketItemEto eventData)
        {
            var productDto = await _productAppService.FindAsync(eventData.ProductId);

            var item = await _repository.FindAsync(x =>
                            x.IdentifierId == eventData.IdentifierId 
                            && x.BasketName == eventData.BasketName 
                            && x.ProductSkuId == eventData.ProductSkuId
                            && x.IsStatic);

            if (eventData.Quantity == 0 || productDto is null)
            {
                if (item != null)
                {
                    await _repository.DeleteAsync(item, autoSave: true);
                }

                return;
            }

            if (item != null)
            {

                await UpdateProductDataAsync(eventData.Quantity, item, productDto, eventData.UnitPrice, eventData.TotalDiscount ?? 0);

                await _repository.UpdateAsync(item, autoSave: true);

                return;
            }

            var productSkuDto = productDto.FindSkuById(eventData.ProductSkuId);

            if (productSkuDto == null)
            {
                /// Exception
                return;
            }

            item = new BasketItem(_guidGenerator.Create(), eventData.TenantId, eventData.BasketName, eventData.IdentifierId,
                productDto.StoreId, eventData.ProductId, eventData.ProductSkuId, true);

            await UpdateProductDataAsync(eventData.Quantity, item, productDto, eventData.UnitPrice, eventData.TotalDiscount ?? 0);

            await _repository.InsertAsync(item, autoSave: true);
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
                IsFixedPrice = productSkuDto.IsFixedPrice,
                Unit = productSkuDto.Unit?.DisplayName
            });

            if (!productDto.IsPublished)
            {
                item.SetIsInvalid(true);
            }

        }
    }
}
