using System.Globalization;
using ATMSystem.Application.Contracts.Admins;
using Spectre.Console;

namespace ATMSystem.Presentation.Console.Scenarios.AdminCommand;

public class AdminAddAccountScenario : IScenario
{
    private readonly IAdminService _adminService;

    public AdminAddAccountScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Add Account";

    public void Run()
    {
        string pin = AnsiConsole.Ask<string>("Enter account pin");

        RegistrationResult result = _adminService.RegistrationAccount(
            Convert.ToInt64(pin, new CultureInfo(1)));

        string message = result switch
        {
            RegistrationResult.Success success =>
                "Registration successful. AccountNumber:" + $" {success.AccountNumber}, Pin: {success.Pin}",
            RegistrationResult.Loss => "Account already registered",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}