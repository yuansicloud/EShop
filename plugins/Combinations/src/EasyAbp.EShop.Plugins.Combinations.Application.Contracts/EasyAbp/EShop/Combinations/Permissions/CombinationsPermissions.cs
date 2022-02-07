using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Plugins.Combinations.Permissions;

public class CombinationsPermissions
{
    public const string GroupName = "Combinations";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CombinationsPermissions));
    }

    public class Combinations
    {
        public const string Default = GroupName + ".Combination";
        public const string CrossStore = Default + ".CrossStore";
        public const string Manage = Default + ".Manage";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
