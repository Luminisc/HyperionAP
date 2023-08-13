using HSAT.Core.DataAccess;

namespace HSAT.DataAccess
{
    public static class DataAccessInitialization
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<IDataAccessService, DataAccessService>();
            return services;
        }
    }
}
