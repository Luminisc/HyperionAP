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
            var dataset = Gdal.Open(@"C:\_Datas\Git\HSAT\_HSI\f120507t01p00r13.tar\f120507t01p00r13rdn_a\f120507t01p00r13rdn_a_sc01_ort_img", Access.GA_ReadOnly);

            var image = new DemoImageFile()
            {
                Dataset = dataset,
            };

            return image;
        }
    }
}
