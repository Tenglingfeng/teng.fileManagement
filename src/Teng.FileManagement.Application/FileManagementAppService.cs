using Teng.FileManagement.Localization;
using Volo.Abp.Application.Services;

namespace Teng.FileManagement
{
    public abstract class FileManagementAppService : ApplicationService
    {
        protected FileManagementAppService()
        {
            LocalizationResource = typeof(FileManagementResource);
            ObjectMapperContext = typeof(FileManagementApplicationModule);
        }
    }
}
