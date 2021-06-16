using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class CreateUpdateProductUnitDto
    {
        [Required]
        [DisplayName("ProductAttributeDisplayName")]
        public string DisplayName { get; set; }

    }
}