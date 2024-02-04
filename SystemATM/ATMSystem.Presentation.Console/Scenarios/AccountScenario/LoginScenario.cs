using System.Globalization;
using ATMSystem.Application.Contracts.UsersMode;
using Spectre.Console;

namespace ATMSystem.Presentation.Console.Scenarios.AccountScenario;

public class LoginScenario : IScenario
{
    private readonly IModeService _accountService;

    public LoginScenario(IModeService accountService)
    {
        _accountService = accountService;
    }

    public string Name => "Login";

    public void Run()
    {
        string accountNumber = AnsiConsole.Ask<string>("Enter your Account Number");
        string pin = AnsiConsole.Ask<string>("Enter your Pin");

        LoginResult result = _accountService.LoginUser(
            Convert.ToInt64(accountNumber, new CultureInfo(1)),
            Convert.ToInt64(pin, new CultureInfo(1)));

        string message = result switch
        {
            LoginResult.Success => "Successful login",
            LoginResult.NotFound => "Account not found",
            LoginResult.InvalidPin=> "Invalid pin",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}