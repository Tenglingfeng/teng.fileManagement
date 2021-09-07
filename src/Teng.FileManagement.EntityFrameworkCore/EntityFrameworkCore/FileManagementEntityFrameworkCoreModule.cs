using Microsoft.Extensions.DependencyInjection;
using Teng.FileManagement.Files;
using Teng.FileManagement.Repositories;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Teng.FileManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(FileManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class FileManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<FileManagementDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */

                options.AddRepository<File, EfCoreFileRepository>();
            });
        }
    }
}