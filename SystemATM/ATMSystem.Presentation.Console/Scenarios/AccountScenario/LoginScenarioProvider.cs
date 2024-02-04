using System.Diagnostics.CodeAnalysis;
using ATMSystem.Application.Contracts.UsersMode;

namespace ATMSystem.Presentation.Console.Scenarios.AccountScenario;

public class LoginScenarioProvider : IScenarioProvider
{
    private readonly IModeService _service;
    private readonly ICurrentModeService _currentModeService;

    public LoginScenarioProvider(IModeService service, ICurrentModeService currentModeService)
    {
        _service = service;
        _currentModeService = currentModeService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentModeService.UserMode is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginScenario(_service);
        return true;
    }
}