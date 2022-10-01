using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class ServiceCollectionExtensions
{
    public static void AddCoreServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));
        
        serviceCollection.AddTransient<IPubsSearchService, PubsSearchService>();
    }
}
