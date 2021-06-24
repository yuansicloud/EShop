using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductUnitRepository : IRepository<ProductUnit, Guid>
    {
    }
}