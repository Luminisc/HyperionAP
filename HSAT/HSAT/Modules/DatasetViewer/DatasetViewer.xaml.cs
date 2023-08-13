using CommunityToolkit.Mvvm.Input;
using HSAT.Core;
using HSAT.Core.Imaging;
using SkiaSharp;
using System.Windows.Input;

namespace HSAT.Modules.DatasetViewer;

public partial class DatasetViewer : ContentPage
{
    private readonly DatasetViewerViewModel viewModel;
    private IDataset dataset;
    private ICommand ScrollCommand;

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
        viewModel.OnRedraw += (_, _) => Redraw();

        // Workaround to fix `null` in binding properties of behaviors
        // https://github.com/xamarin/Xamarin.Forms/issues/2581
        foreach (var behavior in bandNumber.Behaviors)
            behavior.BindingContext = BindingContext;
    }

    private void Redraw()
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

        switch (viewModel.Mode)
        {
            case VisualizationMode.AsIs:
                Render_AsIs(bandWidth, bandHeight, band, bitmap, buffer);
                break;
            case VisualizationMode.MinMaxScale:
                Render_MinMaxScale(bandWidth, bandHeight, band, bitmap, buffer);
                break;
            case VisualizationMode.MeanStddevScale:
                Render_MeanStddevScale(bandWidth, bandHeight, band, bitmap, buffer);
                break;
        }

        this.canvas.SetBitmap(bitmap);
    }

    private static void Render_AsIs(int bandWidth, int bandHeight, IBand band, SKBitmap bitmap, short[] buffer)
    {
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
                    var colorb = (byte)(256 * color / short.MaxValue);
                    unsafePtr[y * bandWidth + x] = (uint)new SKColor(colorb, colorb, colorb);
                }
            }
        }
    }

    private static void Render_MinMaxScale(int bandWidth, int bandHeight, IBand band, SKBitmap bitmap, short[] buffer)
    {
        band.ComputeStatistics(false, out var min, out var max, out var mean, out var stddev);
        var scale = max - min;
        var pixelsPtr = bitmap.GetPixels();
        unsafe
        {
            var unsafePtr = (uint*)pixelsPtr.ToPointer();
            for (var y = 0; y < bandHeight; y++)
            {
                for (var x = 0; x < bandWidth; x++)
                {
                    int color = buffer[y * bandWidth + x];
                    var scaleVal = color - min;
                    var colorb = (byte)(256 * (scaleVal / scale));
                    unsafePtr[y * bandWidth + x] = (uint)new SKColor(colorb, colorb, colorb);
                }
            }
        }
    }

    private static void Render_MeanStddevScale(int bandWidth, int bandHeight, IBand band, SKBitmap bitmap, short[] buffer)
    {
        band.ComputeStatistics(false, out var min, out var max, out var mean, out var stddev);
        var minScale = (int)(mean - stddev);
        var maxScale = (int)(mean + stddev);
        var scale = maxScale - minScale;
        var pixelsPtr = bitmap.GetPixels();
        unsafe
        {
            var unsafePtr = (uint*)pixelsPtr.ToPointer();
            for (var y = 0; y < bandHeight; y++)
            {
                for (var x = 0; x < bandWidth; x++)
                {
                    int color = buffer[y * bandWidth + x];
                    color = Math.Clamp(color, minScale, maxScale) - minScale;
                    var colorb = (byte)(256 * color / scale);
                    unsafePtr[y * bandWidth + x] = (uint)new SKColor(colorb, colorb, colorb);
                }
            }
        }
    }

    protected override void OnDisappearing()
    {
        canvas.Dispose();
        base.OnDisappearing();
    }

    private void visualizationMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        Redraw();
    }

    private void bandNumber_Unfocused(object sender, FocusEventArgs e)
    {
        Redraw();
    }
}