namespace EasyAbp.EShop.Products
{
    public static class ProductsConsts
    {
        public const string DefaultProductGroupName = "Default";
        
        public const string DefaultProductGroupDisplayName = "默认商品";
        
        public const string DefaultProductGroupDescription = "该商品组不进行任何操作";

        public const string CategoryRouteBase = "/api/e-shop/products/category";
        
        public const string GetCategorySummaryListedDataSourceUrl = CategoryRouteBase + "/summary";
        
        public const string GetCategorySummarySingleDataSourceUrl = CategoryRouteBase + "/{id}";
        
        public const string DefaultPaymentExpireInSettingName = "EasyAbp.EShop.Products.Product.DefaultPaymentExpireIn";
    }
}