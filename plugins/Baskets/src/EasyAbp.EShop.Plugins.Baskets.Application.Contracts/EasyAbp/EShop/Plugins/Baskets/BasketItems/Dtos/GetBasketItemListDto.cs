using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos
{
    [Serializable]
    public class GetBasketItemListDto : PagedAndSortedResultRequestDto
    {

        public string BasketName { get; set; } = BasketsConsts.DefaultBasketName;
        
        /// <summary>
        /// Specify the basket item owner user ID. Use current user ID if this property is null.
        /// </summary>
        public Guid? IdentifierId { get; set; }
    }
}