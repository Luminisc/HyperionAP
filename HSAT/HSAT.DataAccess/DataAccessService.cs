using HSAT.Core.DataAccess;
using HSAT.Core.Imaging;
using MaxRev.Gdal.Core;
using OSGeo.GDAL;

namespace HSAT.DataAccess
{
    internal class DataAccessService : IDataAccessService
    {
        static DataAccessService()
        {
            Gdal.AllRegister();
            GdalBase.ConfigureAll();
        }

        public IDataset LoadDataset(string datasetPath)
        {
            var gdalDataset = Gdal.Open(datasetPath, Access.GA_ReadOnly);
            return new Dataset(gdalDataset);
        }
    }
}
