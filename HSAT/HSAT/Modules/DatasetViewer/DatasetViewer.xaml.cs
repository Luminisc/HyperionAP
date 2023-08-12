namespace HSAT.Modules.DatasetViewer;

public partial class DatasetViewer : ContentPage
{
    private readonly DatasetViewerViewModel viewModel;

    public DatasetViewer()
    {
        InitializeComponent();
        this.viewModel = new();
        BindingContext = this.viewModel;
    }

    private void bandNumber_Unfocused(object sender, FocusEventArgs e)
    {
        if (!viewModel.IsValid)
            return;

    }
}