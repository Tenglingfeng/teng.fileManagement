using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Teng.FileManagement.MongoDB
{
    [ConnectionStringName(FileManagementDbProperties.ConnectionStringName)]
    public interface IFileManagementMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
