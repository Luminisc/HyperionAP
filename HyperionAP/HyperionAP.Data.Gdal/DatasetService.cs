using MaxRev.Gdal.Core;
using OSGeo.GDAL;
using SkiaSharp;
using System;
using GDAL = OSGeo.GDAL.Gdal;

namespace HyperionAP.Data.Gdal
{
    public class DatasetService
    {
        static DatasetService()
        {
            GDAL.AllRegister();
            GdalBase.ConfigureAll();
        }

        public void OpenDataset()
        {
            
        }

        public byte[] GetDemoImageBand(string filePath, int bandIndex)
        {
            using var dataset = GDAL.Open(filePath, Access.GA_ReadOnly);
            using var band = dataset.GetRasterBand(bandIndex);
            if (band.DataType != DataType.GDT_UInt16 && band.DataType != DataType.GDT_Int16)
            {
                throw new NotSupportedException();
            }

            int width = dataset.RasterXSize;
            int height = dataset.RasterYSize;
            int bandCount = dataset.RasterCount;

            if (bandIndex < 1 || bandIndex > bandCount)
                throw new ArgumentException("Bad index");
            var buffer = new short[width * height];
            band.ReadRaster(0, 0, width, height, buffer, width, height, 0, 0);

            double min, max;
            band.GetMinimum(out min, out int hasMin);
            band.GetMaximum(out max, out int hasMax);
            if (hasMin == 0 || hasMax == 0)
            {
                ComputeStatistics(buffer, out min, out max);
            }

            using var bitmap = new SKBitmap(width, height, SKColorType.Gray8, SKAlphaType.Opaque);
            var pixels = bitmap.GetPixelSpan();
            var intensityRange = max - min;
            for (int i = 0; i < buffer.Length; i++)
            {
                var normalized = (buffer[i] - min) / intensityRange;
                pixels[i] = (byte)(normalized * 255);
            }

            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = new MemoryStream();
            data.SaveTo(stream);
            return stream.ToArray();
        }

        private static void ComputeStatistics(short[] data, out double min, out double max)
        {
            min = ushort.MaxValue;
            max = ushort.MinValue;

            foreach (var value in data)
            {
                if (value < min) min = value;
                if (value > max) max = value;
            }

            if (min == max)
            {
                min = Math.Max(0, min - 1);
                max = min + 2;
            }
        }
    }
}
