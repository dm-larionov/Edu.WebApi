using Edu.WebApi.Application.Common.Persistence;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.Common.Contracts;
using Edu.WebApi.Domain.EducationProcess;
using Edu.WebApi.Infrastructure.Common;
using Edu.WebApi.Infrastructure.Persistence.ConnectionString;
using Edu.WebApi.Infrastructure.Persistence.Context;
using Edu.WebApi.Infrastructure.Persistence.Initialization;
using Edu.WebApi.Infrastructure.Persistence.Repository;
using Edu.WebApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Serilog;

namespace Edu.WebApi.Infrastructure.Persistence;

internal static class Startup
{
    private static readonly ILogger _logger = Log.ForContext(typeof(Startup));

    internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<DatabaseSettings>()
            .BindConfiguration(nameof(DatabaseSettings))
            .PostConfigure(databaseSettings =>
            {
                _logger.Information("Current DB Provider: {dbProvider}", databaseSettings.DBProvider);
            })
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services
            .AddDbContext<ApplicationDbContext>((p, m) =>
            {
                var databaseSettings = p.GetRequiredService<IOptions<DatabaseSettings>>().Value;
                m.UseDatabase(databaseSettings.DBProvider, databaseSettings.ConnectionString);
            })

            .AddTransient<IDatabaseInitializer, DatabaseInitializer>()
            .AddTransient<ApplicationDbInitializer>()
            .AddTransient<ApplicationDbSeeder>()
            .AddServices(typeof(ICustomSeeder), ServiceLifetime.Transient)
            .AddTransient<CustomSeederRunner>()

            .AddTransient<IConnectionStringSecurer, ConnectionStringSecurer>()
            .AddTransient<IConnectionStringValidator, ConnectionStringValidator>()

            .AddRepositories();
    }

    internal static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string dbProvider, string connectionString)
    {
        switch (dbProvider.ToLowerInvariant())
        {
            case DbProviderKeys.Npgsql:
                return builder.UseNpgsql(connectionString, e =>
                     e.MigrationsAssembly("Migrators.PostgreSQL"));

            case DbProviderKeys.SqlServer:
                return builder.UseSqlServer(connectionString, e =>
                     e.MigrationsAssembly("Migrators.MSSQL"));

            case DbProviderKeys.MySql:
                return builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), e =>
                     e.MigrationsAssembly("Migrators.MySQL")
                      .SchemaBehavior(MySqlSchemaBehavior.Ignore));

            case DbProviderKeys.Oracle:
                return builder.UseOracle(connectionString, e =>
                     e.MigrationsAssembly("Migrators.Oracle"));

            default:
                throw new InvalidOperationException($"DB Provider {dbProvider} is not supported.");
        }
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
            .AddTransient<IAudienceRepository, AudienceRepository>()
            .AddTransient<IAudienceTypeRepository, AudienceTypeRepository>()
            .AddTransient<ICathedraRepository, CathedraRepository>()
            .AddTransient<IDisciplineRepository, DisciplineRepository>()
            .AddTransient<IDisciplineScheduleReplacementRepository, DisciplineScheduleReplacementRepository>()
            .AddTransient<IDisciplineScheduleRepository, DisciplineScheduleRepository>()
            .AddTransient<IDisciplineSemesterRepository, DisciplineSemesterRepository>()
            .AddTransient<IEducationCycleRepository, EducationCycleRepository>()
            .AddTransient<IEducationModuleRepository, EducationModuleRepository>()
            .AddTransient<IEducationFormRepository, EducationFormRepository>()
            .AddTransient<IEducationLevelRepository, EducationLevelRepository>()
            .AddTransient<IEducationPlanRepository, EducationPlanRepository>()
            .AddTransient<IEmployeeRepository, EmployeeRepository>()
            .AddTransient<IFixedDisciplineRepository, FixedDisciplineRepository>()
            .AddTransient<IFixedDisciplineStatusRepository, FixedDisciplineStatusRepository>()
            .AddTransient<IFsesCategoryPartitionRepository, FsesCategoryPartitionRepository>()
            .AddTransient<IIntermediateCertificationFormRepository, IntermediateCertificationFormRepository>()
            .AddTransient<IPostRepository, PostRepository>()
            .AddTransient<IReceivedEducationFormRepository, ReceivedEducationFormRepository>()
            .AddTransient<IReceivedEducationRepository, ReceivedEducationRepository>()
            .AddTransient<IReceivedSpecialtyRepository, ReceivedSpecialtyRepository>()
            .AddTransient<IStudentGroupNameChangeRepository, StudentGroupNameChangeRepository>()
            .AddTransient<IStudentGroupRepository, StudentGroupRepository>()
            .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

        // Add Repositories
        services.AddScoped(typeof(IRepository<>), typeof(ApplicationDbRepository<>));

        services.AddScoped(typeof(IRepositoryAud<>), typeof(ApplicationDbAudRepository<>));


        foreach (var aggregateRootType in
                 typeof(IAggregateRoot).Assembly.GetExportedTypes()
                     .Where(t => typeof(IAggregateRoot).IsAssignableFrom(t) && t.IsClass)
                     .ToList())
        {
            // Add ReadRepositories.
            services.AddScoped(typeof(IReadRepository<>).MakeGenericType(aggregateRootType), sp =>
                sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)));

            // Decorate the repositories with EventAddingRepositoryDecorators and expose them as IRepositoryWithEvents.
            services.AddScoped(typeof(IRepositoryWithEvents<>).MakeGenericType(aggregateRootType), sp =>
                Activator.CreateInstance(
                    typeof(EventAddingRepositoryDecorator<>).MakeGenericType(aggregateRootType),
                    sp.GetRequiredService(typeof(IRepository<>).MakeGenericType(aggregateRootType)))
                ?? throw new InvalidOperationException($"Couldn't create EventAddingRepositoryDecorator for aggregateRootType {aggregateRootType.Name}"));
        }

        foreach (var aggregateRootType in
                 typeof(IAuditableEntity).Assembly.GetExportedTypes()
                     .Where(t => typeof(IAuditableEntity).IsAssignableFrom(t) && t.IsClass)
                     .ToList())
        {
            // Add ReadRepositories.
            services.AddScoped(typeof(IReadAudRepository<>).MakeGenericType(aggregateRootType), sp =>
                sp.GetRequiredService(typeof(IRepositoryAud<>).MakeGenericType(aggregateRootType)));

        }

        return services;
    }
}