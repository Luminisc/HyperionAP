using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HSAT.Validators;
using Microsoft.UI.Xaml.Input;

namespace HSAT.Modules.DatasetViewer
{
    public partial class DatasetViewerViewModel : ObservableValidator
    {
        public event EventHandler OnRedraw;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [LessThan(nameof(MaxBand), "", true)]
        private int bandNumber = 1;

        [ObservableProperty]
        private VisualizationMode mode;

        [ObservableProperty]
        private int maxBand = 1;

        public List<string> Modes { get; } = Enum.GetNames<VisualizationMode>().ToList();

        public DatasetViewerViewModel()
        {
            ValidateAllProperties();
        }

        [RelayCommand]
        public void ScrollBand(PointerRoutedEventArgs e)
        {
            var delta = e.GetCurrentPoint(null).Properties.MouseWheelDelta;
            var i = delta > 0 ? 1 : -1;
            var bandNum = Math.Clamp(BandNumber + i, 1, MaxBand);
            BandNumber = bandNum;
            OnRedraw?.Invoke(this, EventArgs.Empty);
        }
    }

    public enum VisualizationMode : int
    {
        AsIs = 0,
        MinMaxScale = 1,
        MeanStddevScale = 2,
    }
}
