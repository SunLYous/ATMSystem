using System.Globalization;
using ATMSystem.Application.Contracts.UsersMode;
using Spectre.Console;

namespace ATMSystem.Presentation.Console.Scenarios.AdminLogin;

public class AdminLoginScenario : IScenario
{
    private readonly IModeService _modeService;

    public AdminLoginScenario(IModeService modeService)
    {
        _modeService = modeService;
    }

    public string Name => "Login Admin";

    public void Run()
    {
        string pin = AnsiConsole.Ask<string>("Enter admin pin");

        LoginResult result = _modeService.LoginAdmin(Convert.ToInt64(pin, new CultureInfo(1)));

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