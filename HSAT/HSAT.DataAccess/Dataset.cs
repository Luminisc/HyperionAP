using HSAT.Core.Imaging;

namespace HSAT.DataAccess
{
    internal class Dataset : IDataset
    {
        OSGeo.GDAL.Dataset gdalDataset;

        public Dataset(OSGeo.GDAL.Dataset gdalDataset)
        {
            this.gdalDataset = gdalDataset;
        }

        public int BandsCount => gdalDataset.RasterCount;

        public int Width => gdalDataset.RasterXSize;

        public int Height => gdalDataset.RasterYSize;

        public IBand GetBand(int bandNum)
        {
            var band = gdalDataset.GetRasterBand(bandNum);
            return new Band(band);
        }
    }
}
