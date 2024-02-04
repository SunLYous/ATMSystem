using ATMSystem.Presentation.Console.Scenarios.AccountScenario;
using ATMSystem.Presentation.Console.Scenarios.AccountScenario.OperationScenario;
using ATMSystem.Presentation.Console.Scenarios.AdminCommand;
using ATMSystem.Presentation.Console.Scenarios.AdminLogin;
using ATMSystem.Presentation.Console.Scenarios.HistoryScenario;
using Microsoft.Extensions.DependencyInjection;

namespace ATMSystem.Presentation.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<IScenarioProvider, LoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AdminLoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AdminAddAccountProvider>();
        collection.AddScoped<IScenarioProvider, AdminExitScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AccountExitScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AdminChangePasswordScenarioProvider>();
        collection.AddScoped<IScenarioProvider, BalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawScenarioProvider>();
        collection.AddScoped<IScenarioProvider, TopMoneyScenarioProvider>();
        collection.AddScoped<IScenarioProvider, GetHistoryScenarioProvider>();
        collection.AddScoped<ScenarioRunner>();

        return collection;
    }
}