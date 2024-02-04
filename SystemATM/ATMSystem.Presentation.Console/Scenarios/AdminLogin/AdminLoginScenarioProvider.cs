using System.Diagnostics.CodeAnalysis;
using ATMSystem.Application.Contracts.UsersMode;

namespace ATMSystem.Presentation.Console.Scenarios.AdminLogin;

public class AdminLoginScenarioProvider : IScenarioProvider
{
    private readonly IModeService _service;
    private readonly ICurrentModeService _currentModeService;

    public AdminLoginScenarioProvider(
        IModeService service,
        ICurrentModeService currentModeService)
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

        scenario = new AdminLoginScenario(_service);
        return true;
    }
}