namespace EasyAbp.EShop.Inventory.Instocks
{
    using System;

    /// <summary>
    /// 入库类型
    /// </summary>
    [Flags]
    public enum InstockType
    {
        /// <summary>
        /// 采购
        /// </summary>
        Purchase = 1,

        /// <summary>
        /// 盘盈
        /// </summary>
        InventoryProfit = 2,

        /// <summary>
        /// 订单退货
        /// </summary>
        OrderRefund = 4,

        /// <summary>
        /// 数据矫正
        /// </summary>
        DataRepair = 8,

        /// <summary>
        /// 其他
        /// </summary>
        Other = 16
    }
}
