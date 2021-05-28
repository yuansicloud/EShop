namespace EasyAbp.EShop.Stores.Stores.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Volo.Abp.ObjectExtending;

    /// <summary>
    /// 创建及更新店铺模型
    /// </summary>
    [Serializable]
    public class CreateUpdateStoreDto : ExtensibleObject
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        [Required]
        [DisplayName("StoreName")]
        public string Name { get; set; }

        /// <summary>
        /// 店铺地址
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// 店铺图片
        /// </summary>
        public string MediaResources { get; set; }

        /// <summary>
        /// 店铺管理员IDs
        /// </summary>
        public List<Guid> StoreOwnerIds { get; set; } = new List<Guid>();
    }
}
