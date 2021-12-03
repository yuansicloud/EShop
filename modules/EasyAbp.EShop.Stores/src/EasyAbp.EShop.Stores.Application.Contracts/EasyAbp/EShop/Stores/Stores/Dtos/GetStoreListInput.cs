using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.Stores.Dtos
{
    public class GetStoreListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 只获取当前用户可以管理的店铺
        /// Store owner users can get stores that they owns.
        /// Users who have the <code>EasyAbp.EShop.Stores.Store.CrossStore</code> permission can get all stores.
        /// </summary>
        public bool OnlyManageable { get; set; }

        public bool? IsRetail { get; set; }
    }
}