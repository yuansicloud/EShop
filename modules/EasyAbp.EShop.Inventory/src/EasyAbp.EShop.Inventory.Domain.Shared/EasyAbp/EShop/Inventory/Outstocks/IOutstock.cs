namespace EasyAbp.EShop.Inventory.Outstocks
{
    using EasyAbp.EShop.Stores.Stores;
    using System;

    /// <summary>
    /// 出库单
    /// </summary>
    public interface IOutstock : IMultiStore
    {
        /// <summary>
        /// 出库时间
        /// </summary>
        DateTime OutstockTime { get; }

        /// <summary>
        /// 商品SKU ID
        /// </summary>
        Guid ProductSkuId { get; }

        /// <summary>
        /// 商品SPU
        /// </summary>
        Guid ProductId { get; }

        /// <summary>
        /// 单价
        /// </summary>
        decimal UnitPrice { get; }

        /// <summary>
        /// 出库人
        /// </summary>
        string OperatorName { get; }

        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 出库类型
        /// </summary>
        OutstockType OutstockType { get; }

        /// <summary>
        /// 出库单号
        /// </summary>
        string OutstockNumber { get; }

        /// <summary>
        /// 产品组
        /// </summary>
        string ProductGroupName { get; }

        /// <summary>
        /// 产品组显示名称
        /// </summary>
        string ProductGroupDisplayName { get; }

        /// <summary>
        /// 产品编号
        /// </summary>
        string ProductUniqueName { get; }

        /// <summary>
        /// 产品显示名称
        /// </summary>
        string ProductDisplayName { get; }

        /// <summary>
        /// SKU名称
        /// </summary>
        string SkuName { get; }

        /// <summary>
        /// SKU描述
        /// </summary>
        string SkuDescription { get; }

        /// <summary>
        /// 图片
        /// </summary>
        string MediaResources { get; }

        /// <summary>
        /// 货币
        /// </summary>
        string Currency { get; }
        /// <summary>
        /// 数量
        /// </summary>
        int Quantity { get; }

        /// <summary>
        /// 单位
        /// </summary>
        string Unit { get; }
    }
}
