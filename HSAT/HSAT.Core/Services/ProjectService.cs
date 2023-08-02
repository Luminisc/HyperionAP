using Newtonsoft.Json;
using System.Text;

namespace HSAT.Core.Services
{
    public class ProjectService
    {
        /// <summary>Create project file with specified params</summary>
        /// <param name="projectName"></param>
        /// <param name="projectFolder"></param>
        /// <param name="imagePath"></param>
        public async Task Create(string projectName, string projectFolder, string imagePath)
        { 
            var project = Project.Create(projectName, projectFolder, imagePath);
            var projJson = SerializeProject(project);

            var filepath = Path.Combine(projectFolder, projectName, Consts.ProjectExtension);
            using var sw = new StreamWriter(filepath, Encoding.UTF8, new FileStreamOptions() { Mode = FileMode.Create });

            await sw.WriteAsync(projJson);
        }

        private static string SerializeProject(Project project)
        {
            return JsonConvert.SerializeObject(project);
        }
    }
}
