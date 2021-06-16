using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class ProductUnitDto : AuditedEntityDto<Guid>
    {
        public string DisplayName { get; set; }
    }
}
