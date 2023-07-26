using HSAT.Core;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.Diagnostics;

namespace HSAT.Experiments.DemoDrawing;

public partial class DemoDrawing : ContentPage
{
    ImageFile image { get; set; }
    SKBitmap bitmap { get; set; }

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

        if (image != null)
        {
            if (this.bitmap != null)
            {
                canvas.DrawBitmap(this.bitmap, 0f, 0f);
                return;
            }

            var sw = new Stopwatch();
            sw.Start();
            var band = image.Dataset.GetRasterBand(image.Dataset.RasterCount / 2 + 5);
            var bandWidth = band.XSize;
            var bandHeight = band.YSize;
            var bitmap = new SKBitmap(bandWidth, bandHeight);
            // var datatype = band.DataType;
            var buffer = new byte[bandWidth * bandHeight * sizeof(ushort)];
            var readResult = band.ReadRaster(0, 0, bandWidth, bandHeight, buffer, bandWidth, bandHeight, 0, 0);
            Debug.WriteLine($"Read band in: {sw.ElapsedMilliseconds} ms");
            sw.Restart();
            if (readResult != OSGeo.GDAL.CPLErr.CE_None)
            {
                Debug.WriteLine("Oh no");
                bitmap.Dispose();
                return;
            }

            var shortBuffer = new ushort[bandWidth * bandHeight];
            Buffer.BlockCopy(buffer, 0, shortBuffer, 0, buffer.Length);

            var pixelsPtr = bitmap.GetPixels();
            unsafe
            {
                var unsafePtr = (uint*)pixelsPtr.ToPointer();
                for (var y = 0; y < bandHeight; y++)
                {
                    for (var x = 0; x < bandWidth; x++)
                    {
                        int color = shortBuffer[x + y * bandWidth];
                        var colorb = (byte)(color * 256 / ushort.MaxValue);
                        unsafePtr[x + y * bandWidth] = (uint)new SKColor(colorb, colorb, colorb);
                    }
                }
            }

            sw.Stop();
            Debug.WriteLine($"Loaded in: {sw.ElapsedMilliseconds} ms");
            this.bitmap = bitmap;
            canvas.DrawBitmap(bitmap, 0f, 0f);
        }
        else
        {
            using var bitmap = new SKBitmap(width, height);
            var random = new Random();
            for (var i = 0; i < 1000; i++)
            {
                bitmap.SetPixel(random.Next(width), random.Next(height), new SKColor((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)));
            }

            canvas.DrawBitmap(bitmap, 0f, 0f);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        image = ImageFile.Load();
        skCanvas.InvalidateSurface();
    }
}