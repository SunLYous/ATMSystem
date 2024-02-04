using System.Diagnostics.CodeAnalysis;
using ATMSystem.Application.Contracts.Admins;
using ATMSystem.Application.Contracts.UsersMode;
using ATMSystem.Application.Models;

namespace ATMSystem.Presentation.Console.Scenarios.AdminCommand;

public class AdminAddAccountProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    private readonly ICurrentModeService _currentModeService;

    public AdminAddAccountProvider(IAdminService service, ICurrentModeService currentModeService)
    {
        _service = service;
        _currentModeService = currentModeService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentModeService.UserMode == UserMode.Admin)
        {
            scenario = new AdminAddAccountScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}