using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Input;
using System.Windows.Input;

namespace HSAT.Modules.DatasetViewer;

public partial class DatasetViewer : ContentPage
{
    public DatasetViewer()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public ICommand DemoCommand { get; set; } = new RelayCommand<PointerRoutedEventArgs>((e) => { });
}