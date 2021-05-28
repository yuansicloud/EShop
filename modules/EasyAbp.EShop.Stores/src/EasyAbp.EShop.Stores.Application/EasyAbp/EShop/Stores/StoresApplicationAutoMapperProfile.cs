﻿using AutoMapper;
using EasyAbp.EShop.Stores.StoreOwners;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Stores;
using EasyAbp.EShop.Stores.Stores.Dtos;
using EasyAbp.EShop.Stores.Transactions;
using EasyAbp.EShop.Stores.Transactions.Dtos;
using Volo.Abp.AutoMapper;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Stores
{
    public class StoresApplicationAutoMapperProfile : Profile
    {
        public StoresApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Store, StoreDto>(MemberList.None);
            CreateMap<CreateUpdateStoreDto, Store>(MemberList.None);

            CreateMap<StoreOwner, StoreOwnerDto>()
                .Ignore(dto => dto.OwnerUserName)
                .Ignore(dto => dto.OwnerName);
            CreateMap<CreateUpdateStoreOwnerDto, StoreOwner>(MemberList.None);
            
            CreateMap<Transaction, TransactionDto>();
            CreateMap<CreateUpdateTransactionDto, Transaction>(MemberList.None);
        }
    }
}
