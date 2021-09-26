using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductSkuRepository : IRepository<ProductSku, Guid>
    {
    }
}