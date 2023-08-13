using HSAT.Core.Imaging;
using OSGeo.GDAL;

namespace HSAT.DataAccess
{
    internal class Band : IBand
    {
        private readonly OSGeo.GDAL.Band band;

        public Band(OSGeo.GDAL.Band band)
        {
            this.band = band;
        }

        public ImageDataType DataType => (ImageDataType)this.band.DataType;

        public OpResult ComputeStatistics(bool approx_ok, out double min, out double max, out double mean, out double stddev)
        {
            var readResult = band.ComputeStatistics(false, out min, out max, out mean, out stddev, null, null);
            return ToResult(readResult);
        }

        public OpResult ReadRaster(int xOffset, int yOffset, int xSize, int ySize, short[] buffer, int buf_xSize, int buf_ySize, int pixelSpace, int lineSpace)
        {
            var readResult = band.ReadRaster(xOffset, yOffset, xSize, ySize, buffer, buf_xSize, buf_ySize, pixelSpace, lineSpace);
            return ToResult(readResult);
        }

        private static OpResult ToResult(CPLErr result)
        {
            return result switch
            {
                CPLErr.CE_None => OpResult.CE_None,
                CPLErr.CE_Log => OpResult.CE_Log,
                CPLErr.CE_Warning => OpResult.CE_Warning,
                CPLErr.CE_Failure => OpResult.CE_Failure,
                CPLErr.CE_Fatal => OpResult.CE_Fatal,
                _ => OpResult.CE_None,
            };
        }
    }
}
