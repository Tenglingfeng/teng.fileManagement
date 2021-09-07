using Teng.FileManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Teng.FileManagement.Permissions
{
    public class FileManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(FileManagementPermissions.GroupName, L("Permission:FileManagement"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<FileManagementResource>(name);
        }
    }
}