﻿namespace EasyAbp.EShop.Plugins.FlashSales;

public static class FlashSalesDbProperties
{
    public static string DbTablePrefix { get; set; } = "EasyAbpEShopPluginsFlashSales";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "EasyAbpEShopPluginsFlashSales";
}
