using Microsoft.Extensions.DependencyInjection;

namespace HyperionAP.Data.Gdal
{
    public class GdalInitializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<DatasetService>();
        }
    }
}
