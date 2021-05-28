namespace EasyAbp.EShop.Stores.StoreOwners.Dtos
{
    using EasyAbp.EShop.Stores.Stores;
    using System;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 店铺管理员模型
    /// </summary>
    [Serializable]
    public class StoreOwnerDto : ExtensibleAuditedEntityDto<Guid>, IMultiStore
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// 店铺用户ID
        /// </summary>
        public Guid OwnerUserId { get; set; }

        /// <summary>
        /// 店铺管理员用户名
        /// </summary>
        public string OwnerUserName { get; set; }

        /// <summary>
        /// 店铺管理员姓名
        /// </summary>
        public string OwnerName { get; set; }
    }
}
