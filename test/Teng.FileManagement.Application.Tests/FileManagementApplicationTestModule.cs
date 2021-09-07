using Volo.Abp.Modularity;

namespace Teng.FileManagement
{
    [DependsOn(
        typeof(FileManagementApplicationModule),
        typeof(FileManagementDomainTestModule)
        )]
    public class FileManagementApplicationTestModule : AbpModule
    {

    }
}
