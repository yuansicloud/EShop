﻿using EasyAbp.EShop;
using EasyAbp.PaymentService.WeChatPay;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace EasyMall
{
    [DependsOn(
        typeof(EasyMallApplicationContractsModule),
        typeof(AbpAccountHttpApiClientModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpPermissionManagementHttpApiClientModule),
        typeof(AbpTenantManagementHttpApiClientModule),
        typeof(AbpFeatureManagementHttpApiClientModule),
        typeof(EShopHttpApiClientModule),
        typeof(PaymentServiceWeChatPayHttpApiClientModule)
    )]
    public class EasyMallHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EasyMallApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
