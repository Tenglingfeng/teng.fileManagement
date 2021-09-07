using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Teng.FileManagement.EntityFrameworkCore;
using Teng.FileManagement.Files;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Teng.FileManagement.Repositories
{
    public class EfCoreFileRepository : EfCoreRepository<IFileManagementDbContext, File, Guid>, IFileRepository
    {
        public EfCoreFileRepository(IDbContextProvider<IFileManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<File> FindByBlobNameAsync(string blobName)
        {
            Check.NotNullOrWhiteSpace(blobName, nameof(blobName));

            return await DbSet.FirstOrDefaultAsync(x => x.BlobName == blobName);
        }
    }
}