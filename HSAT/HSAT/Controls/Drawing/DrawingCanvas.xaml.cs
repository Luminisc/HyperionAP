using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Input;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.Windows.Input;

namespace HSAT.Controls.Drawing;

public partial class DrawingCanvas : ContentView
{
    ICommand ZoomCommand { get; }

    SKBitmap Bitmap { get; set; }
    SKPoint CurrentTranslation { get; set; } = new SKPoint(0, 0);
    SKPoint PrevTranslation { get; set; } = new SKPoint(0, 0);
    double PictureScale { get; set; } = 1.0f;

    public DrawingCanvas()
    {
        InitializeComponent();
        ZoomCommand = new RelayCommand<PointerRoutedEventArgs>(CanvasZoomChanged);
    }

    /// <summary>Set new bitmap for canvas, and dispose previous one</summary>
    public void SetBitmap(SKBitmap bitmap)
    {
        Bitmap?.Dispose();
        Bitmap = bitmap;
        InvalidateSurface();
    }

    public void InvalidateSurface()
    {
        skCanvas.InvalidateSurface();
    }

    private void CanvasPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        if (Bitmap == null)
            return;

        var surface = e.Surface;
        var canvas = surface.Canvas;
        int width = e.Info.Width;
        int height = e.Info.Height;

        canvas.Clear();
        int resizedWidth = (int)(Bitmap.Width * PictureScale),
            resizedHeight = (int)(Bitmap.Height * PictureScale);

        using var resizedBitmap = Bitmap.Resize(new SKImageInfo(resizedWidth, resizedHeight), SKFilterQuality.None);

        var translation = CurrentTranslation + new SKPoint((width - resizedWidth) / 2, (height - resizedHeight) / 2);
        canvas.DrawBitmap(resizedBitmap, translation);
    }

    private void CanvasPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Running:
                CurrentTranslation = PrevTranslation + new SKPoint((float)e.TotalX, (float)e.TotalY);
                break;
            case GestureStatus.Completed:   
                PrevTranslation = CurrentTranslation;
                break;
            case GestureStatus.Canceled:
                CurrentTranslation = PrevTranslation;
                break;
        }
        InvalidateSurface();
    }

    private void CanvasZoomChanged(PointerRoutedEventArgs e)
    {
        PictureScale += e.GetCurrentPoint(null).Properties.MouseWheelDelta / 1000.0f;
        PictureScale = Math.Clamp(PictureScale, double.Epsilon, 1000);
        InvalidateSurface();
    }
}