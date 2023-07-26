using HSAT.Core;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System.Diagnostics;

namespace HSAT.Experiments.DemoDrawing;

public partial class DemoDrawing : ContentPage
{
    DemoImageFile Image { get; set; }
    SKBitmap Bitmap { get; set; }

    public DemoDrawing()
    {
        InitializeComponent();
    }

    private void SKCanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var surface = e.Surface;
        var canvas = surface.Canvas;
        int width = e.Info.Width;
        int height = e.Info.Height;

        if (Image != null)
        {
            if (this.Bitmap != null)
            {
                // canvas.DrawBitmap(this.bitmap, 0f, 0f);
                return;
            }

            canvas.Clear();
            var sw = new Stopwatch();
            sw.Start();
            var band = Image.Dataset.GetRasterBand(Image.Dataset.RasterCount / 2 + 5);
            var bandWidth = band.XSize;
            var bandHeight = band.YSize;
            var bitmap = new SKBitmap(bandWidth, bandHeight);
            var datatype = band.DataType;
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
                        int color = shortBuffer[y * bandWidth + x];
                        var colorb = (byte)(color * 256 / ushort.MaxValue);
                        unsafePtr[y * bandWidth + x] = (uint)new SKColor(colorb, colorb, colorb);
                    }
                }
            }

            sw.Stop();
            Debug.WriteLine($"Loaded in: {sw.ElapsedMilliseconds} ms");
            this.Bitmap = bitmap;
            canvas.DrawBitmap(bitmap, 0f, 0f);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Image = DemoImageFile.Load();
        skCanvas.InvalidateSurface();
    }
}