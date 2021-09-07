using System.Threading.Tasks;
using Shouldly;
using Teng.FileManagement.Dtos;
using Xunit;

namespace Teng.FileManagement
{
    public class FileAppServiceTests : FileManagementApplicationTestBase
    {
        private readonly IFileAppService _fileAppService;

        public FileAppServiceTests()
        {
            _fileAppService = GetRequiredService<IFileAppService>();
        }

        [Fact]
        public async Task Create_FindByBlobName_Test()
        {
            var blobName = await _fileAppService.CreateAsync(new FileDto()
            {
                FileName = "BaGet.zip",
                Bytes = await System.IO.File.ReadAllBytesAsync(@"D:\ProjectInNet\BaGet.zip")
            });
            blobName.ShouldNotBeEmpty();

            var fileDto = await _fileAppService.FindByBlobNameAsync(blobName);
            fileDto.ShouldNotBeNull();

            fileDto.FileName.ShouldBe("BaGet.zip");
        }
    }
}