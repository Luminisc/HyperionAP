using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HSAT.Core;
using HSAT.Core.Services;
using HSAT.Services;
using System.ComponentModel.DataAnnotations;

namespace HSAT.Menus.CreateProject
{
    public partial class CreateProjectViewModel : ObservableValidator
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private string projectName;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private string projectPath;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private string imagePath;

        private readonly ProjectService projectService;
        private readonly IFileService fileService;

        public CreateProjectViewModel(ProjectService projectService, IFileService fileService)
        {
            this.projectService = projectService;
            this.fileService = fileService;
            ValidateAllProperties();
        }

        [RelayCommand]
        public async Task SaveProject()
        {
            // TODO: Warn if project exist
            var path = await this.projectService.Create(ProjectName, ProjectPath, ImagePath);
            var project = await this.projectService.Load(path);
            ProjectContext.Instance.LoadProject(project);
        }

        [RelayCommand]
        public async Task SelectProjectPath()
        {
            var folder = await fileService.OpenFolderDialog();
            if (folder != null && folder.IsSuccessful)
            {
                ProjectPath = folder.Folder.Path;
            }
        }

        [RelayCommand]
        public async Task SelectImagePath()
        {
            var file = await fileService.OpenFileDialog();
            if (file != null)
            {
                ImagePath = file.FullPath;
            }
        }
    }
}