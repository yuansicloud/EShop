namespace EasyAbp.EShop.Stores.StoreOwners.Dtos
{
    using EasyAbp.EShop.Stores.Stores;
    using System;
    using System.ComponentModel.DataAnnotations;
    using Volo.Abp.ObjectExtending;

    /// <summary>
    /// 创建更新店铺管理员模型
    /// </summary>
    [Serializable]
    public class CreateUpdateStoreOwnerDto : ExtensibleObject, IMultiStore
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        [Required]
        public Guid StoreId { get; set; }

        /// <summary>
        /// 管理员ID
        /// </summary>
        [Required]
        public Guid OwnerUserId { get; set; }
    }
}
