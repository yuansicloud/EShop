using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public class DuplicatedCombinationUniqueNameException : BusinessException
    {
        public DuplicatedCombinationUniqueNameException(string uniqueName) : base("DuplicatedCombinationUniqueName",
             $"The combination unique name \"{uniqueName}\" is duplicated.")
        {

        }
    }
}
