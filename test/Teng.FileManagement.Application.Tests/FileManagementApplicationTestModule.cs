using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Modularity;

namespace Teng.FileManagement
{
    [DependsOn(
        typeof(FileManagementApplicationModule),
        typeof(FileManagementDomainTestModule),
        typeof(AbpBlobStoringFileSystemModule)
        )]
    public class FileManagementApplicationTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBlobStoringOptions>(x =>
            {
                x.Containers.ConfigureDefault(t =>
                {
                    t.UseFileSystem(c =>
                        {
                            c.BasePath = @"D:\ProjectInNet\Teng\teng.file-management\my-files";
                        });
                });
            });

            base.ConfigureServices(context);
        }
    }
}