namespace EasyAbp.EShop.Stores.Stores.Dtos
{
    using EasyAbp.EShop.Stores.StoreOwners.Dtos;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 店铺模型
    /// </summary>
    [Serializable]
    public class StoreDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        [Required]
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
        /// 店铺管理员
        /// </summary>
        public List<StoreOwnerDto> StoreOwners { get; set; }

        public bool IsRetail { get; set; }
    }
}
