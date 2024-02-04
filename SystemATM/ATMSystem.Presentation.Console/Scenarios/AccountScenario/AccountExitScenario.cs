using ATMSystem.Application.UsersMode;

namespace ATMSystem.Presentation.Console.Scenarios.AccountScenario;
public class AccountExitScenario : IScenario
{
    private readonly CurrentModeService _service;

    public AccountExitScenario(CurrentModeService service)
    {
        _service = service;
    }

    public string Name => "Exit";
    public void Run()
    {
        _service.UserMode = null;
    }
}