using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos
{
    public class GetCombinationListInput : PagedAndSortedResultRequestDto
    {

        public string Filter { get; set; }

        public decimal? MinimumPrice { get; set; }

        public decimal? MaximumPrice { get; set; }
    }
}
