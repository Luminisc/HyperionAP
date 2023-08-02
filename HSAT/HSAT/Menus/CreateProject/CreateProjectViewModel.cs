using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HSAT.Core.Services;
using HSAT.Services;

namespace HSAT.Menus.CreateProject
{
    public partial class CreateProjectViewModel : ObservableObject
    {
        [ObservableProperty]
        private string projectName;
        [ObservableProperty]
        private string projectPath;
        [ObservableProperty]
        private string imagePath;

        private ProjectService projectService;
        private IFileService fileService;

        public CreateProjectViewModel(ProjectService projectService, IFileService fileService)
        {
            this.projectService = projectService;
            this.fileService = fileService;
        }

        [RelayCommand]
        public async Task SaveProject()
        {
            System.Diagnostics.Debug.WriteLine("Project saving...");
            await Task.CompletedTask;
        }

        [RelayCommand]
        public async Task SelectProjectPath()
        {
            var file = await fileService.OpenFileDialog();
            if (file != null)
            {
                ProjectPath = file.FullPath;
            }
        }
    }
}