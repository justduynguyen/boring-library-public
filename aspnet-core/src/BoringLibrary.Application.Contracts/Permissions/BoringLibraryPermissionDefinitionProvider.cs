using BoringLibrary.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace BoringLibrary.Permissions;

public class BoringLibraryPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BoringLibraryPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BoringLibraryPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BoringLibraryResource>(name);
    }
}