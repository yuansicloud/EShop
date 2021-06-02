namespace EasyAbp.EShop.Inventory.Outstocks
{
    using System;

    /// <summary>
    /// 出库类型
    /// </summary>
    [Flags]
    public enum OutstockType
    {
        /// <summary>
        /// 售卖
        /// </summary>
        Sale = 1,

        /// <summary>
        /// 盘亏
        /// </summary>
        InventoryLoss = 2,

        /// <summary>
        /// 报损
        /// </summary>
        Damage = 4,

        /// <summary>
        /// 内部消耗
        /// </summary>
        InternalUsage = 8,

        /// <summary>
        /// 其他
        /// </summary>
        Other = 16
    }
}
