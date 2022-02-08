﻿using System;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos
{
    public class GetCombinationListInput
    {
        public Guid? StoreId { get; set; }

        public string Filter { get; set; }

        public decimal? MinimumPrice { get; set; }

        public decimal? MaximumPrice { get; set; }
    }
}