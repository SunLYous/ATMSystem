using ATMSystem.Application.UsersMode;

namespace ATMSystem.Presentation.Console.Scenarios.AdminCommand;

public class AdminExitScenario : IScenario
{
    private readonly CurrentModeService _service;

    public AdminExitScenario(CurrentModeService service)
    {
        _service = service;
    }

    public string Name => "Exit";
    public void Run()
    {
        _service.UserMode = null;
    }
}