using Infrastructure.CsvData;
using Infrastructure.Data;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Interfaces;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

        serviceCollection.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase(new Guid().ToString());
        });

        // un-comment below lines to use the SQL database connection
        //var connectionString = configuration.GetConnectionString("DefaultConnection");
        //serviceCollection.AddDbContext<AppDbContext>(optionsBuilder => { optionsBuilder.UseSqlServer(connectionString); });

        serviceCollection.AddTransient<IRepository, EfRepository>();
        serviceCollection.AddTransient<IPubsDataSeeder, PubsDataSeeder>();
        serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<PubsMapProfile>());
    }
}