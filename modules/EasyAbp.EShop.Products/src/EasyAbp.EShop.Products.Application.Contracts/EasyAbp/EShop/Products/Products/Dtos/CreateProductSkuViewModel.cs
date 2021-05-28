﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class CreateProductSkuViewModel : ExtensibleObject
    {
        [DisplayName("ProductSkuName")]
        public string Name { get; set; }

        [Required]
        [DisplayName("ProductSkuCurrency")]
        public string Currency { get; set; }

        [DisplayName("ProductSkuOriginalPrice")]
        public decimal? OriginalPrice { get; set; }

        [DisplayName("ProductSkuPrice")]
        public decimal Price { get; set; }

        [DefaultValue(1)]
        [DisplayName("ProductSkuOrderMinQuantity")]
        public int OrderMinQuantity { get; set; }

        [DefaultValue(99)]
        [DisplayName("ProductSkuOrderMaxQuantity")]
        public int OrderMaxQuantity { get; set; }

        [DisplayName("ProductSkuMediaResources")]
        public string MediaResources { get; set; }

        [DisplayName("ProductSkuProductDetailId")]
        public Guid? ProductDetailId { get; set; }

        /// <summary>
        /// 可否调价（true 为不可调价）
        /// </summary>
        public bool IsFixedPrice { get; set; }

        public ICollection<ProductAttributeOptionViewModel> AttributeOptions { get; set; }
    }

    public class ProductAttributeOptionViewModel
    {
        public string Attribute { get; set; }

        public string AttributeOption { get; set; }
    }
}
