using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace HSAT.Controls.Drawing;

public partial class DrawingCanvas : ContentView, IDisposable
{
    DrawingCanvasViewModel viewModel;

    public DrawingCanvas()
    {
        InitializeComponent();
        viewModel = new();
        BindingContext = viewModel;
        viewModel.OnSurfaceInvalidation += (_, _) => { InvalidateSurface(); };

        // Workaround to fix `null` in binding properties of behaviors
        // https://github.com/xamarin/Xamarin.Forms/issues/2581
        foreach (var behavior in skCanvas.Behaviors)
            behavior.BindingContext = BindingContext;
    }

    /// <summary>Set new bitmap for canvas, and dispose previous one</summary>
    public void SetBitmap(SKBitmap bitmap) => viewModel.SetBitmap(bitmap);

    public void Dispose() => viewModel.Dispose();

    private void CanvasPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        if (viewModel.Bitmap == null)
            return;

        var Bitmap = viewModel.Bitmap;
        var surface = e.Surface;
        var canvas = surface.Canvas;
        int width = e.Info.Width;
        int height = e.Info.Height;

        canvas.Clear();
        int resizedWidth = (int)(Bitmap.Width * viewModel.PictureScale),
            resizedHeight = (int)(Bitmap.Height * viewModel.PictureScale);

        using var resizedBitmap = Bitmap.Resize(new SKImageInfo(resizedWidth, resizedHeight), SKFilterQuality.None);

        var translation = viewModel.CurrentTranslation + new SKPoint((width - resizedWidth) / 2, (height - resizedHeight) / 2);
        canvas.DrawBitmap(resizedBitmap, translation);
    }

    private void CanvasPanUpdated(object sender, PanUpdatedEventArgs e) => viewModel.CanvasPanUpdated(sender, e);

    private void InvalidateSurface() => skCanvas.InvalidateSurface();
}