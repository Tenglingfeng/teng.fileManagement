using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teng.FileManagement.Dtos;
using Volo.Abp;
using Volo.Abp.Http;

namespace Teng.FileManagement
{
    [RemoteService]
    [Route("api/file-management/files")]
    public class FileController : FileManagementController
    {
        private readonly IFileAppService _fileAppService;

        public FileController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        [HttpGet]
        [Route("{blobName}")]
        public virtual async Task<FileResult> GetAsync(string blobName)
        {
            var fileDto = await _fileAppService.FindByBlobNameAsync(blobName);
            return File(fileDto.Bytes, MimeTypes.GetByExtension(Path.GetExtension(fileDto.FileName)));
        }

        [HttpPost]
        [Route("upload")]
        [Authorize]
        public virtual async Task<JsonResult> CreateAsync(IFormFile file)
        {
            if (file == null)
            {
                throw new UserFriendlyException("No file found!");
            }

            var bytes = await file.GetAllBytesAsync();
            var result = await _fileAppService.CreateAsync(new FileDto()
            {
                Bytes = bytes,
                FileName = file.FileName
            });
            return Json(result);
        }
    }
}