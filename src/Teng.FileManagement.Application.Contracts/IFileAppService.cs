using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teng.FileManagement.Dtos;
using Volo.Abp.Application.Services;

namespace Teng.FileManagement
{
    public interface IFileAppService : IApplicationService
    {
        Task<byte[]> GetAsync(string name);

        Task<string> CreateAsync(FileUploadInputDto input);

        Task<FileDto> FindByBlobNameAsync(string blobName);

        Task<string> CreateAsync(FileDto input);
    }
}