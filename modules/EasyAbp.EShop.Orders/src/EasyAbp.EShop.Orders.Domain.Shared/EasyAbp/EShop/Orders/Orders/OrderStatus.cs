namespace EasyAbp.EShop.Orders.Orders
{
    using System;

    /// <summary>
    /// Defines the OrderStatus.
    /// </summary>
    [Flags]
    public enum OrderStatus
    {
        /// <summary>
        /// 待处理
        /// </summary>
        Pending = 1,

        /// <summary>
        /// 处理中
        /// </summary>
        Processing = 2,

        /// <summary>
        /// 处理完成
        /// </summary>
        Completed = 4,

        /// <summary>
        /// 处理失败
        /// </summary>
        Canceled = 8
    }
}
