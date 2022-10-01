using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Interfaces;

namespace Infrastructure.UnitTests.Data;

public abstract class EfRepositoryTestBase
{
    protected AppDbContext DbContext;

    protected static DbContextOptions<AppDbContext> CreateNewContextOptions()
    {
        // Create a fresh service provider, and therefore a fresh
        // InMemory database instance.
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        // Create a new options instance telling the context to use an
        // InMemory database and the new service provider.
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseInMemoryDatabase("leedsBeerQuest")
            .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }

    protected IRepository GetRepository()
    {
        var options = CreateNewContextOptions();

        DbContext = new AppDbContext(options);
        return new EfRepository(DbContext);
    }
}