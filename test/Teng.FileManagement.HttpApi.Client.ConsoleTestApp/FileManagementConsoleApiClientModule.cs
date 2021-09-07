using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Teng.FileManagement
{
    [DependsOn(
        typeof(FileManagementHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class FileManagementConsoleApiClientModule : AbpModule
    {
        
    }
}
