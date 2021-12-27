namespace EasyAbp.EShop.Inventory.StockHistories
{
    using EasyAbp.EShop.Stores.Stores;
    using System;

    /// <summary>
    /// 库存存品历史记录
    /// </summary>
    public interface IStockHistory : IMultiStore
    {
        /// <summary>
        /// Gets the LockedQuantity
        /// 锁定库存数量.
        /// </summary>
        int LockedQuantity { get; }

        /// <summary>
        /// 商品SPU
        /// </summary>
        Guid ProductId { get; }

        /// <summary>
        /// 商品SKU
        /// </summary>
        Guid ProductSkuId { get; }

        /// <summary>
        /// 库存量
        /// </summary>
        int Quantity { get; }

        /// <summary>
        /// 变化量
        /// </summary>
        int AdjustedQuantity { get; }

        /// <summary>
        /// 备注
        /// </summary>
        string Description { get; }

    }
}
