using EasyAbp.EShop.Plugins.Combinations.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.EShop.Plugins.Combinations.Permissions;

public class CombinationsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CombinationsPermissions.GroupName, L("Permission:Combinations"));

            var combinationPermission = myGroup.AddPermission(CombinationsPermissions.Combinations.Default, L("Permission:Combination"));
            combinationPermission.AddChild(CombinationsPermissions.Combinations.Create, L("Permission:Create"));
            combinationPermission.AddChild(CombinationsPermissions.Combinations.Update, L("Permission:Update"));
            combinationPermission.AddChild(CombinationsPermissions.Combinations.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CombinationsResource>(name);
    }
}
