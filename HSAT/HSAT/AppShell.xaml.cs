using CommunityToolkit.Maui.Views;
using HSAT.Menus.CreateProject;

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
}
