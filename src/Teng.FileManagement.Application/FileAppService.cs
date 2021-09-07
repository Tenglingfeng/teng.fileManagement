using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Teng.FileManagement.Dtos;
using Teng.FileManagement.Files;
using Teng.FileManagement.Settings;
using Volo.Abp;
using Volo.Abp.Settings;
using Volo.Abp.Validation;
using File = System.IO.File;

namespace Teng.FileManagement
{
    public class FileAppService : FileManagementAppService, IFileAppService
    {
        private readonly IFileManager _fileManager;
        private readonly FileOptions _fileOptions;

        public FileAppService(IOptions<FileOptions> fileOptions, IFileManager fileManager)
        {
            _fileManager = fileManager;
            _fileOptions = fileOptions.Value;
        }

        public Task<byte[]> GetAsync(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var filePath = Path.Combine(_fileOptions.FileUploadLocalFolder, name);

            if (File.Exists(filePath))
            {
                return Task.FromResult(File.ReadAllBytes(filePath));
            }

            return Task.FromResult(new byte[0]);
        }

        [Authorize]
        public Task<string> CreateAsync(FileUploadInputDto input)
        {
            if (input.Bytes.IsNullOrEmpty())
            {
                throw new AbpValidationException("Bytes can not be null or empty!",
                    new List<ValidationResult>
                    {
                        new ValidationResult("Bytes can not be null or empty!", new[] {"Bytes"})
                    });
            }

            if (input.Bytes.Length > _fileOptions.MaxFileSize)
            {
                throw new UserFriendlyException($"File exceeds the maximum upload size ({_fileOptions.MaxFileSize / 1024 / 1024} MB)!");
            }

            if (!_fileOptions.AllowedUploadFormats.Contains(Path.GetExtension(input.Name)))
            {
                throw new UserFriendlyException("Not a valid file format!");
            }

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(input.Name);
            var filePath = Path.Combine(_fileOptions.FileUploadLocalFolder, fileName);

            if (!Directory.Exists(_fileOptions.FileUploadLocalFolder))
            {
                Directory.CreateDirectory(_fileOptions.FileUploadLocalFolder);
            }

            File.WriteAllBytes(filePath, input.Bytes);

            return Task.FromResult("/api/file-management/files/" + fileName);
        }

        public virtual async Task<FileDto> FindByBlobNameAsync(string blobName)
        {
            Check.NotNullOrWhiteSpace(blobName, nameof(blobName));
            var file = await _fileManager.FindByBlobNameAsync(blobName);
            var bytes = await _fileManager.GetBlobAsync(blobName);

            return new FileDto()
            {
                FileName = file.FileName,
                Bytes = bytes
            };
        }

        public virtual async Task<string> CreateAsync(FileDto input)
        {
            await CheckFile(input);
            var file = await _fileManager.CreateAsync(input.FileName, input.Bytes);
            return file.BlobName;
        }

        protected virtual async Task CheckFile(FileDto input)
        {
            if (input.Bytes.IsNullOrEmpty())
            {
                throw new AbpValidationException("Bytes can not be null or empty!", new List<ValidationResult>()
                {
                    new ValidationResult("Bytes can not be null or empty!", new[] {"Bytes"})
                });
            }

            var allowedMaxFileSize = await SettingProvider.GetAsync<int>(FileManagementSettings.AllowedMaxFileSize);
            var allowedUploadFormats =
                (await SettingProvider.GetOrNullAsync(FileManagementSettings.AllowedUploadFormats))
                ?.Split(",", StringSplitOptions.RemoveEmptyEntries);

            if (input.Bytes.Length > allowedMaxFileSize * 1024)
            {
                throw new UserFriendlyException(L["FileManagement.ExceedsTheMaximumSize", allowedMaxFileSize]);
                //throw new UserFriendlyException("文件太大了");
            }

            if (allowedUploadFormats == null || !allowedUploadFormats.Contains(Path.GetExtension(input.FileName)))

            {
                //throw new UserFriendlyException(L["FileManagement.NotValidFormat"]);
                throw new UserFriendlyException($"{allowedUploadFormats.JoinAsString(",")},{Path.GetExtension(input.FileName)}");
            }
        }
    }
}