using CommunityToolkit.Maui.Views;
using HSAT.Core;
using HSAT.Core.Services;
using HSAT.Menus.CreateProject;
using HSAT.Services;

namespace HSAT;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    private async void CreateProject(object sender, EventArgs e)
    {
        var popup = Handler.MauiContext.Services.GetRequiredService<CreateProjectPopup>();
        await this.ShowPopupAsync(popup);
    }

    private async void OpenProject(object sender, EventArgs e)
    {
        var fileService = Handler.MauiContext.Services.GetRequiredService<IFileService>();
        var filePath = await fileService.OpenFileDialog();

        if (filePath != null)
        {
            var service = Handler.MauiContext.Services.GetRequiredService<ProjectService>();
            var project = await service.Load(filePath.FullPath);
            ProjectContext.Instance.LoadProject(project);
        }
    }
}
