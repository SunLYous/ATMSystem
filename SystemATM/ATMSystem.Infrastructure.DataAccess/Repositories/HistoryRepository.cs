using ATMSystem.Application.Abstractions.Repositories;
using ATMSystem.Application.Contracts.Histories;
using ATMSystem.Application.Models.Histories;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace ATMSystem.Infrastructure.DataAccess.Repositories;

public class HistoryRepository : IHistoryRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public HistoryRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<IEnumerable<History>> GetHistories(long accountId)
    {
        const string sql = """
                           select *
                           from history
                           where account_id = :accountId
                           """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("accountId", accountId);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        var result = new List<History>();

        while (await reader.ReadAsync())
        {
            result.Add(new History(
                Id: reader.GetInt64(0),
                DateTime: reader.GetDateTime(1),
                OperationType: await reader.GetFieldValueAsync<OperationType>(2),
                Value: reader.GetInt64(3),
                Balance: reader.GetInt64(4)));
        }

        return result;
    }

    public async Task<CreateHistoryRowResult> AddHistory(
        long accountId,
        OperationType operationType,
        long balance,
        long value)
    {
        const string sql = """
                           INSERT INTO history (account_id, operation, value, balance)
                           VALUES (:accountId, :operation, :value, :balance)
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("accountId", accountId);
        command.AddParameter("operation", operationType);
        command.AddParameter("value", value);
        command.AddParameter("balance", balance);
        int row = await command.ExecuteNonQueryAsync(default);
        if (row == 0)
            return new CreateHistoryRowResult.Loss();
        return new CreateHistoryRowResult.Success();
    }
}