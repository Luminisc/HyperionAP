using HyperionAP.Data.Gdal;
using HyperionAP.Infrastructure;
using HyperionAP.UI.Server.Services;

var builder = WebApplication.CreateBuilder(args);

InfrastructureRegistration.AddDbContext(builder);
InfrastructureRegistration.AddRepositories(builder.Services);

// Initialize services
builder.Services.AddScoped<FilesService>();
GdalInitializer.Initialize(builder.Services);

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // NSwag installation guide
    // https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-8.0&tabs=visual-studio
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
