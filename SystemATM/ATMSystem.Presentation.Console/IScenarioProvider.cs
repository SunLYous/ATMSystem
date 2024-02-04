using System.Diagnostics.CodeAnalysis;
using ATMSystem.Presentation.Console.Scenarios;

namespace ATMSystem.Presentation.Console;

public interface IScenarioProvider
{
    bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}