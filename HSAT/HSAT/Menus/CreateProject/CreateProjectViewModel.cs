using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HSAT.Core.Services;

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

        private ProjectService projectService { get; }

        public CreateProjectViewModel(ProjectService projectService)
        {
            this.projectService = projectService;
        }

        [RelayCommand]
        public async Task SaveProject()
        {
            System.Diagnostics.Debug.WriteLine("Project saving...");
            await Task.CompletedTask;
        }
    }
}