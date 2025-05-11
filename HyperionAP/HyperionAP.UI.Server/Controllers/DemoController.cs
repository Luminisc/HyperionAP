using HyperionAP.Data.Gdal;
using HyperionAP.UI.Server.Services;
using Microsoft.AspNetCore.Mvc;
using FileInfo = HyperionAP.UI.Server.Services.FileInfo;

namespace HyperionAP.UI.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DemoController(FilesService filesService, DatasetService datasetService) : ControllerBase
    {
        [HttpPost]
        public async Task<FileInfo> Upload(IFormFile file)
        {
            var fileInfo = await filesService.AddFile(file);
            return fileInfo;
        }

        [HttpGet]
        public IEnumerable<FileInfo> GetFiles()
        {
            return filesService.GetFiles();
        }

        [HttpGet]
        public async Task<IActionResult> GetImageBand(Guid fileId, int band)
        {
            var filePath = filesService.GetDatasetPath(fileId);
            var pngBytes = datasetService.GetDemoImageBand(filePath, band);
            return File(pngBytes, "image/png", "output.png");
        }
    }
}
