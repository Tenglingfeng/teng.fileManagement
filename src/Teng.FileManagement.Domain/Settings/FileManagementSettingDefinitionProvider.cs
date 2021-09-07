using Volo.Abp.Settings;

namespace Teng.FileManagement.Settings
{
    public class FileManagementSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from FileManagementSettings class.
             */

            context.Add(new SettingDefinition(FileManagementSettings.GroupName),
                new SettingDefinition(FileManagementSettings.AllowedMaxFileSize, "100000"),
                new SettingDefinition(FileManagementSettings.AllowedUploadFormats, ".zip,.jpg"));
        }
    }
}