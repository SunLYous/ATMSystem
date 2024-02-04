using ATMSystem.Application.Accounts;
using ATMSystem.Application.Admins;
using ATMSystem.Application.Contracts.Accounts;
using ATMSystem.Application.Contracts.Admins;
using ATMSystem.Application.Contracts.Histories;
using ATMSystem.Application.Contracts.UsersMode;
using ATMSystem.Application.Histories;
using ATMSystem.Application.UsersMode;
using Microsoft.Extensions.DependencyInjection;

namespace ATMSystem.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IAdminService, AdminService>();
        collection.AddScoped<IModeService, ModeService>();
        collection.AddScoped<IAccountService, AccountService>();
        collection.AddScoped<IHistoryService, HistoryService>();
        collection.AddScoped<CurrentAccountService>();
        collection.AddScoped<ICurrentAccountService>(
            p => p.GetRequiredService<CurrentAccountService>());
        collection.AddScoped<CurrentAdminService>();
        collection.AddScoped<ICurrentAdminService>(
            p => p.GetRequiredService<CurrentAdminService>());
        collection.AddScoped<CurrentModeService>();
        collection.AddScoped<ICurrentModeService>(
            p => p.GetRequiredService<CurrentModeService>());
        return collection;
    }
}