using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Services;

namespace Teng.FileManagement.Files
{
    public class FileManager : DomainService, IFileManager
    {
        private readonly IFileRepository _fileRepository;

        private readonly IBlobContainer _blobContainer;

        public FileManager(IFileRepository fileRepository, IBlobContainer blobContainer)
        {
            _fileRepository = fileRepository;
            _blobContainer = blobContainer;
        }

        public virtual async Task<File> FindByBlobNameAsync(string blobName)
        {
            Check.NotNullOrWhiteSpace(blobName, nameof(blobName));

            return await _fileRepository.FindByBlobNameAsync(blobName);
        }

        public virtual async Task<File> CreateAsync(string fileName, byte[] bytes)
        {
            Check.NotNullOrWhiteSpace(fileName, nameof(fileName));

            var blobName = GuidGenerator.Create().ToString("N");

            var file = await _fileRepository.InsertAsync(new File(GuidGenerator.Create(), CurrentTenant.Id, fileName,
                blobName, bytes.Length));

            await _blobContainer.SaveAsync(blobName, bytes);

            return file;
        }

        public virtual async Task<byte[]> GetBlobAsync(string blobName)
        {
            Check.NotNullOrWhiteSpace(blobName, nameof(blobName));

            return await _blobContainer.GetAllBytesAsync(blobName);
        }
    }
}