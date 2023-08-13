using HSAT.Core;
using HSAT.Core.Imaging;
using SkiaSharp;

namespace HSAT.Modules.DatasetViewer;

public partial class DatasetViewer : ContentPage
{
    private readonly DatasetViewerViewModel viewModel;
    private IDataset dataset;

    public DatasetViewer()
    {
        InitializeComponent();
        this.viewModel = new();
        BindingContext = this.viewModel;
        if (ProjectContext.Instance.Project != null)
        {
            dataset = ProjectContext.Instance.GetDataset();
            viewModel.MaxBand = dataset.BandsCount;
        }

        ProjectContext.ProjectChanged += (_, _) => { dataset = ProjectContext.Instance.GetDataset(); viewModel.MaxBand = dataset.BandsCount; };
    }

    private void bandNumber_Unfocused(object sender, FocusEventArgs e)
    {
        if (viewModel.HasErrors || dataset == null)
            return;

        var bandWidth = dataset.Width;
        var bandHeight = dataset.Height;
        var band = dataset.GetBand(viewModel.BandNumber);
        var bitmap = new SKBitmap(bandWidth, bandHeight);
        // var datatype = band.DataType;

        var buffer = new short[bandWidth * bandHeight];
        var readResult = band.ReadRaster(0, 0, bandWidth, bandHeight, buffer, bandWidth, bandHeight, 0, 0);
        if (readResult != OpResult.CE_None)
        {
            // Debug.WriteLine("Oh no");
            bitmap.Dispose();
            return;
        }

        band.ComputeStatistics(false, out var min, out var max, out var mean, out var stddev);
        var pixelsPtr = bitmap.GetPixels();
        unsafe
        {
            var unsafePtr = (uint*)pixelsPtr.ToPointer();
            for (var y = 0; y < bandHeight; y++)
            {
                for (var x = 0; x < bandWidth; x++)
                {
                    int color = buffer[y * bandWidth + x];
                    if (color < 0)
                        color = 0;
                    var colorb = (byte)(256 * color / max);
                    unsafePtr[y * bandWidth + x] = (uint)new SKColor(colorb, colorb, colorb);
                }
            }
        }

        this.canvas.SetBitmap(bitmap);
    }

    protected override void OnDisappearing()
    {
        canvas.Dispose();
        base.OnDisappearing();
    }
}