using System.Globalization;
using ATMSystem.Application.Contracts.Accounts;
using Spectre.Console;

namespace ATMSystem.Presentation.Console.Scenarios.AccountScenario.OperationScenario;

public class WithdrawScenario : IScenario
{
    private readonly IAccountService _accountService;

    public WithdrawScenario(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Withdraw money";

    public void Run()
    {
        string value = AnsiConsole.Ask<string>("enter the amount");
        OperationResult result = _accountService.WithdrawMoney(Convert.ToInt64(value, new CultureInfo(1)));
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