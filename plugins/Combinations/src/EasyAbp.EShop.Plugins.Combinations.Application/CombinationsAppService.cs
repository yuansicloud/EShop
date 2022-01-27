using EasyAbp.EShop.Plugins.Combinations.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Combinations;

public abstract class CombinationsAppService : ApplicationService
{
    protected CombinationsAppService()
    {
        LocalizationResource = typeof(CombinationsResource);
        ObjectMapperContext = typeof(CombinationsApplicationModule);
    }
}
