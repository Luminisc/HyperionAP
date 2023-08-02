using CommunityToolkit.Maui.Views;

namespace HSAT.Menus.CreateProject;

public partial class CreateProjectPopup : Popup
{
    public CreateProjectViewModel ViewModel { get; set; }

    public CreateProjectPopup(CreateProjectViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        BindingContext = ViewModel;
    }

    private void ClosePopup(object sender, EventArgs e)
    {
        Close();
    }

    private async void CreateProject(object sender, EventArgs e)
    {
        await ViewModel.SaveProjectCommand.ExecuteAsync(null);
    }
}