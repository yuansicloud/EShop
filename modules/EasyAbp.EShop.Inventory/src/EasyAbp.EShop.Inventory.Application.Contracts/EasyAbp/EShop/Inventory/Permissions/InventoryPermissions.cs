using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Inventory.Permissions
{
    public class InventoryPermissions
    {
        public const string GroupName = "Inventory";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(InventoryPermissions));
        }

        public class Inventories
        {
            public const string Default = GroupName + ".Inventory";
            public const string CrossStore = Default + ".CrossStore";
        }

    }
}
