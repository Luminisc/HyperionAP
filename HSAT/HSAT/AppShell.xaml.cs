using CommunityToolkit.Maui.Views;
using HSAT.Menus.CreateProject;
using HSAT.ViewModels;

namespace HSAT;

public partial class AppShell : Shell
{
    ProjectContextViewModel ProjectContext { get; set; } = new ();

    public AppShell()
    {
        InitializeComponent();
    }

    private async void CreateProject(object sender, EventArgs e)
    {
        await this.ShowPopupAsync(new CreateProjectPopup());
    }
}
