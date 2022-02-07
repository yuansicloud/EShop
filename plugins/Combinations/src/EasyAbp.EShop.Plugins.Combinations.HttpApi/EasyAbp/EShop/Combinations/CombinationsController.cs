using EasyAbp.EShop.Plugins.Combinations.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Combinations;

public abstract class CombinationsController : AbpController
{
    protected CombinationsController()
    {
        LocalizationResource = typeof(CombinationsResource);
    }
}
