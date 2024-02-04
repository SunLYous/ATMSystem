using System.Diagnostics.CodeAnalysis;
using ATMSystem.Application.Models;
using ATMSystem.Application.UsersMode;

namespace ATMSystem.Presentation.Console.Scenarios.AccountScenario;

public class AccountExitScenarioProvider : IScenarioProvider
{
    private readonly CurrentModeService _service;

    public AccountExitScenarioProvider(CurrentModeService currentModeService)
    {
        _service = currentModeService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_service.UserMode == UserMode.Account)
        {
            scenario = new AccountExitScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}