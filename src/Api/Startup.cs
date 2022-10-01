using Core;
using Infrastructure;
using SharedKernel.Interfaces;

namespace Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddHealthChecks();   // To-do - add health-check for dependencies

        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddInfrastructureServices(_configuration);
        services.AddCoreServices(_configuration);
    }
    public void Configure(WebApplication app)
    {
        // Don't use the below SeedData code for production
        var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
        using (var scope = scopedFactory?.CreateScope())
        {
            var seeder = scope?.ServiceProvider.GetService<IPubsDataSeeder>();
            seeder? .SeedData();
        }

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}