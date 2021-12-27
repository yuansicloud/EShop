namespace EasyAbp.EShop.Inventory.Instocks.Dtos
{
    using EasyAbp.EShop.Stores.Stores;
    using System;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 入库数据模型
    /// </summary>
    [Serializable]
    public class InstockDto : FullAuditedEntityDto<Guid>, IMultiStore, IInstock
    {
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime InstockTime { get; set; }

        /// <summary>
        /// 商品SKU ID
        /// </summary>
        public Guid ProductSkuId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Units { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public Guid WarehouseId { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        //public Guid SupplierId { get; set; }

        public Guid ProductId { get; set; }

        public InstockType InstockType { get; set; }

        public string InstockNumber { get; set; }

        public string ProductGroupName { get; set; }

        public string ProductGroupDisplayName { get; set; }

        public string ProductUniqueName { get; set; }

        public string ProductDisplayName { get; set; }

        public string SkuName { get; set; }

        public string SkuDescription { get; set; }

        public string MediaResources { get; set; }

        public string Currency { get; set; }

        public int Quantity { get; set; }

        public string Unit { get; set; }
    }
}
