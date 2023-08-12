using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace HSAT.Modules.DatasetViewer
{
    public partial class DatasetViewerViewModel : ObservableValidator
    {
        [ObservableProperty]
        //[NotifyDataErrorInfo]
        //[Required]
        private int bandNumber;

        [ObservableProperty]
        //[NotifyDataErrorInfo]
        //[Required]
        private VisualizationMode mode;

        [ObservableProperty]
        private int maxBand = 0;

        [ObservableProperty]
        private bool isValid;

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
