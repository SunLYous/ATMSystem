using ATMSystem.Application.Abstractions.Repositories;
using ATMSystem.Application.Models.Accounts;
using ATMSystem.Application.Models.Admins;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace ATMSystem.Infrastructure.DataAccess.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AdminRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<Admin?> CheckPassword(long password)
    {
        const string sql = """
                           select *
                           from Admins
                           where admin_password = :password;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("password", password);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if ((await reader.ReadAsync()) is false)
            return null;

        return new Admin(
            Id: reader.GetInt64(0),
            Password: reader.GetInt64(1));
    }

    public async Task ChangeAdminPassword(long id, long newPassword)
    {
        const string sql = """
                           UPDATE admins
                           SET admin_password = :newPassword
                           WHERE admin_id = :id;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);
        command.AddParameter("newPassword", newPassword);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
    }

    public async Task<Account?> AddAccount(long pin)
    {
        const string sql = """
                           INSERT INTO accounts (account_pin, money)
                           VALUES (:pin, 0)
                           RETURNING *;
                           """;
        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("pin", pin);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync() is false)
            return null;

        return new Account(
            AccountNumber: reader.GetInt64(0),
            Pin: reader.GetInt64(1),
            Money: reader.GetInt64(0));
    }
}