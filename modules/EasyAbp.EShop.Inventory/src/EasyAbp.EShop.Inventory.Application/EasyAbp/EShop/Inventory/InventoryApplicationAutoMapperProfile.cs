using AutoMapper;
using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Instocks.Dtos;
using EasyAbp.EShop.Inventory.Outstocks;
using EasyAbp.EShop.Inventory.Outstocks.Dtos;
using EasyAbp.EShop.Inventory.StockHistories;

namespace EasyAbp.EShop.Inventory
{
    public class InventoryApplicationAutoMapperProfile : Profile
    {
        public InventoryApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Instock, InstockDto>();
            CreateMap<Outstock, OutstockDto>();
        }
    }
}