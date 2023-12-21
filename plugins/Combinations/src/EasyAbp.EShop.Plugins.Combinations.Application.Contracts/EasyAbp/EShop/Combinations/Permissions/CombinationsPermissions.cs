using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Plugins.Combinations.Permissions;

public class CombinationsPermissions
{
    public const string GroupName = "EasyAbp.EShop.Plugins.Combinations";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CombinationsPermissions));
    }

    public class Combinations
    {
        public const string Default = GroupName + ".Combination";
        public const string Manage = Default + ".Manage";
    }
}
