using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace Teng.FileManagement.EntityFrameworkCore
{
    public class FileManagementHttpApiHostMigrationsDbContext : AbpDbContext<FileManagementHttpApiHostMigrationsDbContext>
    {
        public FileManagementHttpApiHostMigrationsDbContext(DbContextOptions<FileManagementHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureFileManagement();
            modelBuilder.ConfigureAuditLogging();
            modelBuilder.ConfigurePermissionManagement();
            modelBuilder.ConfigureSettingManagement();
        }
    }
}
