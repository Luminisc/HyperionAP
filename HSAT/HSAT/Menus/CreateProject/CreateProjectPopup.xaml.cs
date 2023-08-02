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
}