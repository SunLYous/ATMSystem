using ATMSystem.Application.Models.Histories;
using Itmo.Dev.Platform.Postgres.Plugins;
using Npgsql;

namespace ATMSystem.Infrastructure.DataAccess.Plugins;

public class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<OperationType>();
    }
}