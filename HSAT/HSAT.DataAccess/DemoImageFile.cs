using MaxRev.Gdal.Core;
using OSGeo.GDAL;

namespace HSAT.Core
{
    public class DemoImageFile
    {
        static DemoImageFile()
        {
            Gdal.AllRegister();
            GdalBase.ConfigureAll();
        }

        public Dataset Dataset { get; set; }

        public static DemoImageFile Load()
        {
            var dataset = Gdal.Open(@"C:\_Datas\Git\HSAT\_HSI\moffet_field\f190802t01p00r12rdn_e_sc01_ort_img", Access.GA_ReadOnly);

            var image = new DemoImageFile()
            {
                Dataset = dataset,
            };

            return image;
        }
    }
}
