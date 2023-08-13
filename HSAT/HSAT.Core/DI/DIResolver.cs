namespace HSAT.Core.DI
{
    public static class DIResolver
    {
        private static IServiceProvider _serviceProvider;
        public static IServiceProvider ServiceProvider => _serviceProvider ?? throw new Exception("Service provider must be initialized");

        public static void RegisterServiceProvider(IServiceProvider sp)
        {
            _serviceProvider = sp;
        }

        public static T Resolve<T>() where T : class => ServiceProvider.GetRequiredService<T>();

        public static IServiceProvider UseDI(this IServiceProvider sp)
        {
            RegisterServiceProvider(sp);
            return sp;
        }
    }
}
