using ATMSystem.Application.Abstractions.Repositories;
using ATMSystem.Infrastructure.DataAccess.Plugins;
using ATMSystem.Infrastructure.DataAccess.Repositories;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.Dev.Platform.Postgres.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace ATMSystem.Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);
        collection.AddSingleton<IDataSourcePlugin, MappingPlugin>();

        collection.AddScoped<IAccountRepository, AccountRepository>();
        collection.AddScoped<IAdminRepository, AdminRepository>();
        collection.AddScoped<IHistoryRepository, HistoryRepository>();

        return collection;
    }
}