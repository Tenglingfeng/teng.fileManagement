using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Teng.FileManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(FileManagementDomainSharedModule)
    )]
    public class FileManagementDomainModule : AbpModule
    {

    }
}
