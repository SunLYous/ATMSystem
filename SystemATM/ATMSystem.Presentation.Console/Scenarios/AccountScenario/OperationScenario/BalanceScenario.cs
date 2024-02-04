using ATMSystem.Application.Contracts.Accounts;
using Spectre.Console;

namespace ATMSystem.Presentation.Console.Scenarios.AccountScenario.OperationScenario;

public class BalanceScenario : IScenario
{
    private readonly IAccountService _accountService;

    public BalanceScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Balance";

    public void Run()
    {
        OperationResult result = _accountService.Balance();

        string message = result switch
        {
            OperationResult.Success success => "Balance:" + $" {success.Money.Value}",

            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}