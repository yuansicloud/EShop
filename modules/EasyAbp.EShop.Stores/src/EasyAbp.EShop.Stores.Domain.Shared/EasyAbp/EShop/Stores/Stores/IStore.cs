using System;

namespace EasyAbp.EShop.Stores.Stores
{
    public interface IStore
    {
        string Name { get; }

        Address Address { get; }

        string MediaResources { get; }
    }
}