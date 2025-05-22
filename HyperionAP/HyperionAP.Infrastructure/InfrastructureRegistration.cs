using HyperionAP.Core.Repositories.Interfaces;
using HyperionAP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace HyperionAP.Infrastructure;

public class InfrastructureRegistration
{
    public static IHostApplicationBuilder AddDbContext(IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var connectionString = configuration.GetConnectionString("Maindb");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        return builder;
    }

    public static IServiceCollection AddRepositories(IServiceCollection services) =>
        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IProjectRepository, ProjectRepository>();
}
