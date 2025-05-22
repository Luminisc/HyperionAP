using HyperionAP.Core.Models;
using HyperionAP.Core.Repositories.Interfaces;
using HyperionAP.Data.Gdal;
using HyperionAP.UI.Server.Services;
using Microsoft.AspNetCore.Mvc;
using FileInfo = HyperionAP.UI.Server.Services.FileInfo;

namespace HyperionAP.UI.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DemoController(FilesService filesService, DatasetService datasetService, IProjectRepository projectRepository, IUserRepository userRepository) : ControllerBase
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

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers() => await userRepository.GetAllUsersAsync();

        [HttpGet]
        public async Task<IEnumerable<Project>> GetProjects() => await projectRepository.GetAllProjectsAsync();

        [HttpGet]
        public async Task<User> AddUser(User user) => await userRepository.AddAsync(user);

        [HttpPost]
        public async Task<Project> AddProject(string name, Guid owner)
        {
            // should be service logic...
            var user = await userRepository.GetAsync(owner);
            if (user == null)
                throw new ArgumentException(nameof(owner));
            var project = await projectRepository.AddAsync(new Project(name, user));
            return project;
        }
    }
}
