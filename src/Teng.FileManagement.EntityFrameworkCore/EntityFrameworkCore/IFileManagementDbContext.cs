using Microsoft.EntityFrameworkCore;
using Teng.FileManagement.Files;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Teng.FileManagement.EntityFrameworkCore
{
    [ConnectionStringName(FileManagementDbProperties.ConnectionStringName)]
    public interface IFileManagementDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */

        DbSet<File> Files { get; set; }
    }
}