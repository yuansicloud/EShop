namespace EasyAbp.EShop.Inventory.Instocks
{
    using EasyAbp.EShop.Stores.Stores;
    using System;

    /// <summary>
    /// 入库接口
    /// </summary>
    public interface IInstock : IMultiStore
    {
        /// <summary>
        /// 入库时间
        /// </summary>
        DateTime InstockTime { get; }

        /// <summary>
        /// 产品SKU ID
        /// </summary>
        Guid ProductSkuId { get; }

        /// <summary>
        /// 商品SPU
        /// </summary>
        Guid ProductId { get; }

        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 单价
        /// </summary>
        decimal UnitPrice { get; }

        /// <summary>
        /// 入库人
        /// </summary>
        string OperatorName { get; }

        /// <summary>
        /// 入库类型
        /// </summary>
        InstockType InstockType { get; }

        /// <summary>
        /// 入库单号
        /// </summary>
        string InstockNumber { get; }

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
