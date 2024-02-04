using System.Diagnostics.CodeAnalysis;
using ATMSystem.Application.Contracts.Accounts;
using ATMSystem.Application.Models;
using ATMSystem.Application.UsersMode;

namespace ATMSystem.Presentation.Console.Scenarios.AccountScenario.OperationScenario;

public class WithdrawScenarioProvider : IScenarioProvider
{
    private readonly CurrentModeService _modeService;
    private readonly IAccountService _accountService;

    public WithdrawScenarioProvider(CurrentModeService modeService, IAccountService accountService)
    {
        _modeService = modeService;
        _accountService = accountService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_modeService.UserMode == UserMode.Account)
        {
            scenario = new WithdrawScenario(_accountService);
            return true;
        }

        scenario = null;
        return false;
    }
}