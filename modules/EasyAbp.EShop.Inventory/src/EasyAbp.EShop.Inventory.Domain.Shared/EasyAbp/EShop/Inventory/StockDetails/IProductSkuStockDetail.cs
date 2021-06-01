using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EShop.Inventory.StockDetails
{
    public interface IProductSkuStockDetail
    {
        /// <summary>
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
        /// 库存数
        /// </summary>
        int Quantity { get; }

        /// <summary>
        /// 库存数
        /// </summary>
        int InstockQuantity { get; }

        /// <summary>
        /// 库存数
        /// </summary>
        int OutStockQuantity { get; }

        /// <summary>
        /// 期初库存数
        /// </summary>
        int? TermStartQuantity { get; }

        /// <summary>
        /// 期末库存数
        /// </summary>
        int? TermEndQuantity { get; }
    }
}
