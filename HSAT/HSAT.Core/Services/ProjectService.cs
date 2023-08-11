using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using static HSAT.Core.Project;

namespace HSAT.Core.Services
{
    public class ProjectService
    {
        /// <summary>Create project file with specified params</summary>
        /// <param name="projectName"></param>
        /// <param name="projectFolder"></param>
        /// <param name="imagePath"></param>
        public async Task<string> Create(string projectName, string projectFolder, string imagePath)
        {
            var project = Project.Create(projectName, projectFolder, imagePath);
            var filepath = GetProjectFilepath(projectName, projectFolder);

            await Save(project);

            return filepath;
        }

        public async Task Save(Project project)
        {
            var projJson = SerializeProject(ProjectSaveModel.FromModel(project));
            var filepath = GetProjectFilepath(project.Name, project.ProjectPath);
            using var fs = new FileStream(filepath, FileMode.Create);
            using var sw = new StreamWriter(fs, Encoding.UTF8);

            await sw.WriteAsync(projJson).ConfigureAwait(false);
        }

        public async Task<Project> Load(string filepath)
        {
            using (var filestream = File.OpenRead(filepath))
            using (var sr = new StreamReader(filestream, Encoding.UTF8))
            {
                var json = await sr.ReadToEndAsync().ConfigureAwait(false);
                var projectSaveModel = DeserializeProject(json);
                return projectSaveModel.ToModel();
            }
        }

        public string GetProjectFilepath(string projectName, string projectFolder) => Path.Combine(projectFolder, $"{projectName}.{Consts.ProjectExtension}");

        private static string SerializeProject(ProjectSaveModel project)
        {
            return JsonConvert.SerializeObject(project);
        }

        private static ProjectSaveModel DeserializeProject(string json)
        {
            return JsonConvert.DeserializeObject<ProjectSaveModel>(json);
        }
    }
}
