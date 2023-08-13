using CommunityToolkit.Mvvm.ComponentModel;
using HSAT.Validators;

namespace HSAT.Modules.DatasetViewer
{
    public partial class DatasetViewerViewModel : ObservableValidator
    {
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
    }

    public enum VisualizationMode : int
    {
        AsIs = 0,
        MinMaxScale = 1,
        MeanStddevScale = 2,
    }
}
