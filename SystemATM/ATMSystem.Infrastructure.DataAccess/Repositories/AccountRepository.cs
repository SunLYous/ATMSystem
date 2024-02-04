using ATMSystem.Application.Abstractions.Repositories;
using ATMSystem.Application.Contracts.Accounts;
using ATMSystem.Application.Models.Accounts;
using ATMSystem.Application.Models.Moneys;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace ATMSystem.Infrastructure.DataAccess.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AccountRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<Account?> FindAccountByAccountNumber(long accountNumber)
    {
        const string sql = """
                           select *
                           from Accounts
                           where account_id = :accountNumber;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("accountNumber", accountNumber);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync() is false)
            return null;

        return new Account(
            AccountNumber: reader.GetInt64(0),
            Pin: reader.GetInt64(1),
            Money: reader.GetInt64(2));
    }

    public async Task<Money?> Balance(long id)
    {
        const string sql = """
                           select money
                           from Accounts
                           where account_id = :id;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        return await reader.ReadAsync() is false ? null : new Money(reader.GetInt64(0));
    }

    public async Task<OperationResult> WithdrawMoney(long id, Money updateMoney)
    {
        const string sql = """
                           UPDATE Accounts
                           SET money = money - :updateMoney.Value
                           WHERE account_id = :id
                           AND money >= :updateMoney.Value
                           RETURNING money;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        command.AddParameter("updateMoney.Value", updateMoney.Value);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return new OperationResult.Loss();
        return new OperationResult.Success(new Money(reader.GetInt64(0)));
    }

    public async Task<Money> TopAccount(long id, Money updateMoney)
    {
        const string sql = """
                           UPDATE Accounts
                           SET money = money + :updateMoney.Value
                           WHERE account_id = :id
                           RETURNING money;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        command.AddParameter("updateMoney.Value", updateMoney.Value);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        return new Money(reader.GetInt64(0));
    }
}