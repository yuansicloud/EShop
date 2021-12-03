using EasyAbp.Abp.Trees;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(EShopProductsDomainModule),
        typeof(EShopProductsApplicationContractsModule),
        typeof(EShopStoresDomainSharedModule),
        typeof(EShopStoresApplicationSharedModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpTreesApplicationModule)
    )]
    public class EShopProductsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            LimitedResultRequestDto.MaxMaxResultCount = int.MaxValue;

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.Configurators.Add(abpAutoMapperConfigurationContext =>
                {
                    var profile = abpAutoMapperConfigurationContext.ServiceProvider
                        .GetRequiredService<ProductsApplicationAutoMapperProfile>();
                    
                    abpAutoMapperConfigurationContext.MapperConfiguration.AddProfile(profile);
                });
            });
        }
    }
}
