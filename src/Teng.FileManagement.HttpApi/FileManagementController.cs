using Teng.FileManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Teng.FileManagement
{
    public abstract class FileManagementController : AbpController
    {
        protected FileManagementController()
        {
            LocalizationResource = typeof(FileManagementResource);
        }
    }
}
