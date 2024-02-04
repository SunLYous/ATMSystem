using System.Diagnostics.CodeAnalysis;
using ATMSystem.Application.Contracts.Histories;
using ATMSystem.Application.Models;
using ATMSystem.Application.UsersMode;

namespace ATMSystem.Presentation.Console.Scenarios.HistoryScenario;

public class GetHistoryScenarioProvider : IScenarioProvider
{
    private readonly CurrentModeService _modeService;
    private readonly IHistoryService _historyService;

    public GetHistoryScenarioProvider(CurrentModeService modeService, IHistoryService historyService)
    {
        _modeService = modeService;
        _historyService = historyService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_modeService.UserMode == UserMode.Account)
        {
            scenario = new GetHistoryScenario(_historyService);
            return true;
        }

        scenario = null;
        return false;
    }
}