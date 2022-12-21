using Edu.WebApi.Shared.Multitenancy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Edu.WebApi.Infrastructure.Persistence.Initialization;

internal class DatabaseInitializer : IDatabaseInitializer
{
    //private readonly TenantDbContext _tenantDbContext;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(/*TenantDbContext tenantDbContext,*/ IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger)
    {
        //_tenantDbContext = tenantDbContext;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task InitializeDatabasesAsync(CancellationToken cancellationToken)
    {

        using var scope = _serviceProvider.CreateScope();

        // Then run the initialization in the new scope
        await scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>()
            .InitializeAsync(cancellationToken);

        _logger.LogInformation("Ready");
    }
}