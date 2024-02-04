using System.Diagnostics.CodeAnalysis;
using ATMSystem.Application.Models;
using ATMSystem.Application.UsersMode;

namespace ATMSystem.Presentation.Console.Scenarios.AdminCommand;

public class AdminExitScenarioProvider : IScenarioProvider
{
    private readonly CurrentModeService _currentModeService;
    private readonly CurrentModeService _service;

    public AdminExitScenarioProvider(CurrentModeService currentModeService, CurrentModeService service)
    {
        _currentModeService = currentModeService;
        _service = service;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentModeService.UserMode == UserMode.Admin)
        {
            scenario = new AdminExitScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}