using HyperionAP.Data.Gdal;
using HyperionAP.UI.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using FileInfo = HyperionAP.UI.Server.Services.FileInfo;

namespace HyperionAP.UI.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DemoController : ControllerBase
    {
        private readonly FilesService filesService;
        private readonly DatasetService datasetService;

        internal DemoController(FilesService filesService, DatasetService datasetService)
        {
            this.filesService = filesService;
            this.datasetService = datasetService;
        }

        [HttpPost]
        public async Task<FileInfo> Upload(IFormFile file)
        {
            var fileInfo = await filesService.AddFile(file);
            return fileInfo;
        }

        [HttpGet]
        public IEnumerable<(Guid fileId, string filename)> GetFiles()
        {
            return filesService.GetFiles();
        }

        [HttpGet]
        public async Task<IActionResult> GetImageBand(Guid fileId, int band)
        {
            var filePath = filesService.GetDatasetPath(fileId);
            //return datasetService.GetImageBand(band);
            return null;
        }
    }
}
