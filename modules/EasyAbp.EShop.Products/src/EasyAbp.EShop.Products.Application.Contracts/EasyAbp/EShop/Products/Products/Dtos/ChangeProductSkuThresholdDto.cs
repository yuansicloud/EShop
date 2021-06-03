using EasyAbp.EShop.Stores.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class ChangeProductSkuThresholdDto
    {
        public Guid ProductSkuId { get; set; }

        public int Threshold { get; set; }
    }
}
