using EasyAbp.EShop.Stores.Stores;
using System;
using System.ComponentModel;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations.Dtos
{
    [Serializable]
    public class CreateCombinationDto : UpdateCombinationDto, IHasExtraProperties
    {

        public ExtraPropertyDictionary ExtraProperties { get; set; }
    }
}