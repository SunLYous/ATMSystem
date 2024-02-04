using System.Diagnostics.CodeAnalysis;
using ATMSystem.Application.Contracts.Admins;
using ATMSystem.Application.Models;
using ATMSystem.Application.UsersMode;

namespace ATMSystem.Presentation.Console.Scenarios.AdminCommand;

public class AdminChangePasswordScenarioProvider : IScenarioProvider
{
    private readonly CurrentModeService _modeService;
    private readonly IAdminService _adminService;

    public AdminChangePasswordScenarioProvider(CurrentModeService modeService, IAdminService adminService)
    {
        _modeService = modeService;
        _adminService = adminService;
    }

    public bool TryGetScenario([NotNullWhen(true)]out IScenario? scenario)
    {
        if (_modeService.UserMode == UserMode.Admin)
        {
            scenario = new AdminChangePasswordScenario(_adminService);
            return true;
        }

        scenario = null;
        return false;
    }
}