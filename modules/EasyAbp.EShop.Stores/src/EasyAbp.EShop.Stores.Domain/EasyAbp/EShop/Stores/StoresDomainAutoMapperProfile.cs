using AutoMapper;
using EasyAbp.EShop.Stores.Stores;

namespace EasyAbp.EShop.Stores
{
    public class StoresDomainAutoMapperProfile : Profile
    {
        public StoresDomainAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Store, StoreEto>();
        }
    }
}
