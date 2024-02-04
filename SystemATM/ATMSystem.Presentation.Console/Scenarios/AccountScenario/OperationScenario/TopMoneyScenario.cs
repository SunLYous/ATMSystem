using System.Globalization;
using ATMSystem.Application.Contracts.Accounts;
using Spectre.Console;

namespace ATMSystem.Presentation.Console.Scenarios.AccountScenario.OperationScenario;

public class TopMoneyScenario : IScenario
{
    private readonly IAccountService _accountService;

    public TopMoneyScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Top money";

    public void Run()
    {
        string value = AnsiConsole.Ask<string>("enter the amount");
        OperationResult result = _accountService.TopAccount(Convert.ToInt64(value, new CultureInfo(1)));
        string message = result switch
        {
            OperationResult.Success success => "Balance:" + $" {success.Money.Value}",
            OperationResult.Loss => "insufficient funds",

            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}