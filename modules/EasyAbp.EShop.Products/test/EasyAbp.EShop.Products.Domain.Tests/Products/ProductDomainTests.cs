using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductDomainTests : ProductsDomainTestBase
    {
        private IProductRepository ProductRepository { get; }
        private IProductManager ProductManager { get; }

        public ProductDomainTests()
        {
            ProductRepository = ServiceProvider.GetRequiredService<IProductRepository>();
            ProductManager = ServiceProvider.GetRequiredService<IProductManager>();
        }

        [Fact]
        public async Task Should_Set_ProductDetailId()
        {
            var product2 = new Product(ProductsTestData.Product2Id, null, ProductsTestData.Store1Id, "Default",
                ProductsTestData.ProductDetails2Id, "Ball", "Ball", null, InventoryStrategy.NoNeed, null, true, false,
                false, null, null, 0);
            await ProductManager.CreateAsync(product2);

            product2 = await ProductRepository.GetAsync(product2.Id);

            product2.ProductDetailId.ShouldBe(ProductsTestData.ProductDetails2Id);
        }

        [Fact]
        public async Task Should_Set_Sku_ProductDetailId()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                var product1 = await ProductRepository.GetAsync(ProductsTestData.Product1Id);
                var sku1 = product1.ProductSkus.Single(x => x.Id == ProductsTestData.Product1Sku1Id);

                sku1.ProductDetailId.ShouldBeNull();

                typeof(ProductSku).GetProperty(nameof(ProductSku.ProductDetailId))!.SetValue(sku1,
                    ProductsTestData.ProductDetails1Id);

                await ProductManager.UpdateAsync(product1);

                product1 = await ProductRepository.GetAsync(product1.Id);
                sku1 = product1.ProductSkus.Single(x => x.Id == ProductsTestData.Product1Sku1Id);

                sku1.ProductDetailId.ShouldBe(ProductsTestData.ProductDetails1Id);
            });
        }

        [Fact]
        public async Task Should_Reuse_ProductDetail()
        {
            var product1 = await ProductRepository.GetAsync(ProductsTestData.Product1Id);

            product1.ProductDetailId.ShouldBe(ProductsTestData.ProductDetails1Id);

            var product2 = new Product(ProductsTestData.Product2Id, null, ProductsTestData.Store1Id, "Default",
                ProductsTestData.ProductDetails2Id, "Ball", "Ball", null, InventoryStrategy.NoNeed, null, true, false,
                false, null, null, 0);

            await ProductManager.CreateAsync(product2);

            product2 = await ProductRepository.GetAsync(product2.Id);

            product2.ProductDetailId.ShouldBe(ProductsTestData.ProductDetails2Id);

            typeof(Product).GetProperty(nameof(Product.ProductDetailId))!.SetValue(product2,
                ProductsTestData.ProductDetails1Id);

            await ProductManager.UpdateAsync(product2);

            product2 = await ProductRepository.GetAsync(product2.Id);

            product2.ProductDetailId.ShouldBe(ProductsTestData.ProductDetails1Id);
        }

        [Fact]
        public async Task Should_Remove_ProductDetailId()
        {
            await Should_Set_ProductDetailId();

            var product2 = await ProductRepository.GetAsync(ProductsTestData.Product2Id);

            product2.ProductDetailId.ShouldNotBeNull();

            typeof(Product).GetProperty(nameof(Product.ProductDetailId))!.SetValue(product2, null);

            await ProductManager.UpdateAsync(product2);

            product2 = await ProductRepository.GetAsync(product2.Id);

            product2.ProductDetailId.ShouldBeNull();
        }

        [Fact]
        public async Task Should_Remove_Sku_ProductDetailId()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                await Should_Set_Sku_ProductDetailId();

                var product1 = await ProductRepository.GetAsync(ProductsTestData.Product1Id);
                var sku1 = product1.ProductSkus.Single(x => x.Id == ProductsTestData.Product1Sku1Id);

                sku1.ProductDetailId.ShouldNotBeNull();

                typeof(ProductSku).GetProperty(nameof(Product.ProductDetailId))!.SetValue(sku1, null);

                await ProductManager.UpdateAsync(product1);

                product1 = await ProductRepository.GetAsync(product1.Id);
                sku1 = product1.ProductSkus.Single(x => x.Id == ProductsTestData.Product1Sku1Id);

                sku1.ProductDetailId.ShouldBeNull();
            });
        }

        [Fact]
        public async Task Should_Create_Sku()
        {
            var product1 = await ProductRepository.GetAsync(ProductsTestData.Product1Id);

            var attributeOptionIds = new List<Guid>
            {
                ProductsTestData.Product1Attribute1Option4Id,
                ProductsTestData.Product1Attribute2Option2Id
            };

            await Should.NotThrowAsync(async () =>
            {
                await ProductManager.CreateSkuAsync(product1, await CreateTestSkuAsync(attributeOptionIds));
            });


            product1.ProductSkus.Count(x => x.AttributeOptionIds.SequenceEqual(attributeOptionIds)).ShouldBe(1);
        }

        [Fact]
        public async Task Should_Throw_If_Create_Sku_With_Incorrect_AttributeOptionIds()
        {
            var product1 = await ProductRepository.GetAsync(ProductsTestData.Product1Id);

            await Should.ThrowAsync<ProductSkuIncorrectAttributeOptionsException>(async () =>
            {
                await ProductManager.CreateSkuAsync(product1, await CreateTestSkuAsync(new List<Guid>
                {
                    ProductsTestData.Product1Attribute1Option1Id // need 2 options but input 1
                }));
            });

            await Should.ThrowAsync<ProductSkuIncorrectAttributeOptionsException>(async () =>
            {
                await ProductManager.CreateSkuAsync(product1, await CreateTestSkuAsync(new List<Guid>
                {
                    ProductsTestData.Product1Attribute1Option1Id,
                    Guid.NewGuid() // a nonexistent option
                }));
            });

            await Should.ThrowAsync<ProductSkuIncorrectAttributeOptionsException>(async () =>
            {
                await ProductManager.CreateSkuAsync(product1, await CreateTestSkuAsync(new List<Guid>
                {
                    ProductsTestData.Product1Attribute1Option1Id,
                    ProductsTestData.Product1Attribute1Option2Id // 2 options from attribute1
                }));
            });
        }

        [Fact]
        public async Task Should_Use_Fake_Inventory_Provider()
        {
            var product2 = new Product(ProductsTestData.Product2Id, null, ProductsTestData.Store1Id, "Default",
                ProductsTestData.ProductDetails2Id, "Ball", "Ball", null, InventoryStrategy.NoNeed, "Fake", true, false,
                false, null, null, 0);

            await ProductManager.CreateAsync(product2);

            var fakeSku = new ProductSku(Guid.NewGuid(), new List<Guid> { Guid.NewGuid() }, null, "USD", null, 0m, 1, 1,
                null, null, null);

            var inventoryDataModel = await ProductManager.GetInventoryDataAsync(product2, fakeSku);

            inventoryDataModel.ShouldNotBeNull();
            inventoryDataModel.Inventory.ShouldBe(9998);
        }

        private async Task<ProductSku> CreateTestSkuAsync(List<Guid> attributeOptionIds)
        {
            return new ProductSku(Guid.NewGuid(), attributeOptionIds,
                "test-sku", "USD", null, 0m, 1, 10, null, null, null);
        }
    }
}