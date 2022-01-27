using EasyAbp.EShop.Plugins.Combinations.Combinations;
using EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos;
using AutoMapper;

namespace EasyAbp.EShop.Plugins.Combinations;

public class CombinationsApplicationAutoMapperProfile : Profile
{
    public CombinationsApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
            CreateMap<Combination, CombinationDto>();
            CreateMap<CreateCombinationDto, Combination>(MemberList.Source);
            CreateMap<UpdateCombinationDto, Combination>(MemberList.Source);
            CreateMap<CombinationItem, CombinationItemDto>();
            CreateMap<CreateCombinationItemDto, CombinationItem>(MemberList.Source);
            CreateMap<UpdateCombinationItemDto, CombinationItem>(MemberList.Source);
    }
}
