using EasyAbp.EShop.Products.Products.Dtos;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductUnitAppService : CrudAppService<ProductUnit, ProductUnitDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateProductUnitDto, CreateUpdateProductUnitDto>,
        IProductUnitAppService
    {
        private readonly IProductUnitRepository _repository;

        public ProductUnitAppService(
            IProductUnitRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}