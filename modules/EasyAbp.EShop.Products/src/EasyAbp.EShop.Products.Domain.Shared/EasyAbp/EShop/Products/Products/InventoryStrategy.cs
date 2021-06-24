using System;

namespace EasyAbp.EShop.Products.Products
{
    [Flags]
    public enum InventoryStrategy
    {
        /// <summary>
        /// 无需库存
        /// </summary>
        NoNeed = 1,
        /// <summary>
        /// 下单后减库存
        /// </summary>
        ReduceAfterPlacing = 2,
        /// <summary>
        /// 付款后减库存
        /// </summary>
        ReduceAfterPayment = 4,
        /// <summary>
        /// 使用后减库存
        /// </summary>
        ReduceAfterConsuming = 8,
    }
}