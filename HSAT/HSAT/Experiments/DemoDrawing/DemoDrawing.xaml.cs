using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace HSAT.Experiments.DemoDrawing;

public partial class DemoDrawing : ContentPage
{
    public DemoDrawing()
    {
        InitializeComponent();
    }

    private void Image_Tapped(object sender, EventArgs e)
    {
        graphicsView.Invalidate();
    }

    private void SKCanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        graphicsView.Invalidate();

        var surface = e.Surface;
        var canvas = surface.Canvas;
        int width = e.Info.Width;
        int height = e.Info.Height;

        canvas.Clear();

        using var bitmap = new SKBitmap(width, height);

        // create a new bitmap using the memory
        var random = new Random();
        for (var i = 0; i < 1000; i++)
        {
            bitmap.SetPixel(random.Next(width), random.Next(height), new SKColor((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)));
        }

        canvas.DrawBitmap(bitmap, 0f, 0f);
    }
}