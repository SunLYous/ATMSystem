using System.Globalization;
using ATMSystem.Application.Contracts.Admins;
using Spectre.Console;

namespace ATMSystem.Presentation.Console.Scenarios.AdminCommand;

public class AdminChangePasswordScenario : IScenario
{
    private readonly IAdminService _adminService;

    public AdminChangePasswordScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Change Password";

    public void Run()
    {
        string pin = AnsiConsole.Ask<string>("Enter admin pin");
        string newPin = AnsiConsole.Ask<string>("Enter new admin pin");

        ChangePasswordResult result = _adminService.ChangePassword(
            Convert.ToInt64(pin, new CultureInfo(1)),
            Convert.ToInt64(newPin, new CultureInfo(1)));

        string message = result switch
        {
            ChangePasswordResult.Success => "Successful change",
            ChangePasswordResult.InvalidPassword => "password invalid",
            ChangePasswordResult.Loss => "Loss Change",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}