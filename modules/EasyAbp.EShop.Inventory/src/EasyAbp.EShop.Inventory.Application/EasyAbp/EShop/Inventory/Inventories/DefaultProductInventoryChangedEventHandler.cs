using EasyAbp.EShop.Inventory.EasyAbp.EShop.Inventory;
using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Outstocks;
using EasyAbp.EShop.Products.ProductInventories;
using EasyAbp.EShop.Products.Products;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Inventory.Inventories
{
    public class DefaultProductInventoryChangedEventHandler
    : IProductInventoryChangedEventHandler,
      ITransientDependency
    {
        private readonly IInstockRepository _instockRepository;
        private readonly IOutstockRepository _outstockRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IProductAppService _productAppService;
        private readonly IProductSkuDescriptionProvider _productSkuDescriptionProvider;
        private readonly IClock _clock;

        public DefaultProductInventoryChangedEventHandler(
            IGuidGenerator guidGenerator,
            IInstockRepository instockRepository,
            IOutstockRepository outstockRepository, 
            IProductAppService productAppService,
            IClock clock, 
            IProductSkuDescriptionProvider productSkuDescriptionProvider)
        {
            _guidGenerator = guidGenerator;
            _instockRepository = instockRepository;
            _outstockRepository = outstockRepository;
            _productAppService = productAppService;
            _clock = clock;
            _productSkuDescriptionProvider = productSkuDescriptionProvider;
        }

        [UnitOfWork]
        public virtual async Task HandleEventAsync(ProductInventoryChangedEto eventData)
        {
            var adjustedQuantity = eventData.NewInventory - eventData.OriginalInventory;

            if (adjustedQuantity == 0)
            {
                return;
            }

            if (adjustedQuantity > 0)
            {
                await HandleInstock(eventData, adjustedQuantity);
            }
            else
            {
                await HandleOutstock(eventData, adjustedQuantity);
            }
        }


        protected async Task HandleInstock(ProductInventoryChangedEto eventData, int adjustedQuantity)
        {
            var product = await _productAppService.GetAsync(eventData.ProductId);
            var productSku = product.GetSkuById(eventData.ProductSkuId);

            var instock = new Instock(
            id:_guidGenerator.Create(),
            instockTime: eventData.GetProperty("InstockTime", _clock.Now),
            productSkuId: productSku.Id,
            description: eventData.GetProperty<string>("Description"),
            unitPrice: eventData.GetProperty<decimal>("UnitPrice", 0),
            operatorName: eventData.GetProperty<string>("OperatorName"),
            storeId: product.StoreId,
            tenantId: eventData.TenantId,
            productId: eventData.ProductId,
            instockType: (InstockType) eventData.GetProperty("InstockType", 1),
            instockNumber: eventData.GetProperty("InstockNumber", await GenerateInstockNumber()),
            productGroupName: product.ProductGroupName,
            productGroupDisplayName: product.ProductGroupDisplayName,
            productUniqueName: product.UniqueName,
            productDisplayName: product.DisplayName,
            skuName: productSku.Name,
            skuDescription: await _productSkuDescriptionProvider.GenerateAsync(product, productSku),
            mediaResources: productSku.MediaResources ?? product.MediaResources,
            currency: productSku.Currency,
            quantity: Math.Abs(adjustedQuantity),
            unit: productSku.UnitDisplayName
            );

            await _instockRepository.InsertAsync(instock);
        }

        protected async Task HandleOutstock(ProductInventoryChangedEto eventData, int adjustedQuantity)
        {
            var product = await _productAppService.GetAsync(eventData.ProductId);
            var productSku = product.GetSkuById(eventData.ProductSkuId);

            var outstock = new Outstock(
            id: _guidGenerator.Create(),
            outstockTime: eventData.GetProperty("OutstockTime", _clock.Now),
            productSkuId: productSku.Id,
            description: eventData.GetProperty<string>("Description"),
            unitPrice: eventData.GetProperty<decimal>("UnitPrice", 0),
            operatorName: eventData.GetProperty<string>("OperatorName"),
            storeId: product.StoreId,
            tenantId: eventData.TenantId,
            productId: eventData.ProductId,
            outstockType: (OutstockType)eventData.GetProperty("OutstockType", 1),
            outstockNumber: eventData.GetProperty("OutstockNumber", await GenerateOutstockNumber()),
            productGroupName: product.ProductGroupName,
            productGroupDisplayName: product.ProductGroupDisplayName,
            productUniqueName: product.UniqueName,
            productDisplayName: product.DisplayName,
            skuName: productSku.Name,
            skuDescription: await _productSkuDescriptionProvider.GenerateAsync(product, productSku),
            mediaResources: productSku.MediaResources ?? product.MediaResources,
            currency: productSku.Currency,
            quantity: Math.Abs(adjustedQuantity),
            unit: productSku.UnitDisplayName
            );

            await _outstockRepository.InsertAsync(outstock);
        }

        protected virtual Task<string> GenerateInstockNumber()
        {
            return Task.FromResult("RK" + _clock.Now.ToString("yyyyMMddHHmmssffff") + RandomHelper.GetRandom(0, 99).ToString("00"));
        }

        protected virtual Task<string> GenerateOutstockNumber()
        {
            return Task.FromResult("CK" + _clock.Now.ToString("yyyyMMddHHmmssffff") + RandomHelper.GetRandom(0, 99).ToString("00"));
        }

    }
}