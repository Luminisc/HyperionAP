using CommunityToolkit.Mvvm.Input;
using HSAT.Core;
using HSAT.Core.Services;
using System.Windows.Input;

namespace HSAT.Menus.CreateProject
{
    public class CreateProjectViewModel
    {
        public Project Project { get; set; }
        public ICommand SaveProjectCommand { get; } = new AsyncRelayCommand(() => { return Task.CompletedTask; });
        public ProjectService ProjectService { get; }

        public CreateProjectViewModel(ProjectService projectService)
        {
            ProjectService = projectService;
        }
    }
}