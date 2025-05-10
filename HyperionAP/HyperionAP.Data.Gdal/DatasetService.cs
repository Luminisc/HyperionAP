using MaxRev.Gdal.Core;
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
    }
}
