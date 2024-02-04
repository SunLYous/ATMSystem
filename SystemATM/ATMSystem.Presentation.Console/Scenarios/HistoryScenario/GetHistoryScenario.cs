using System.Globalization;
using ATMSystem.Application.Contracts.Histories;
using ATMSystem.Application.Models.Histories;
using Spectre.Console;

namespace ATMSystem.Presentation.Console.Scenarios.HistoryScenario;

public class GetHistoryScenario : IScenario
{
    private readonly IHistoryService _service;

    public GetHistoryScenario(IHistoryService service)
    {
        _service = service;
    }

    public string Name => "Check history";

    public void Run()
    {
        IEnumerable<History> result = _service.GetHistories();
        var table = new Table();
        table.AddColumn("Operation");
        table.AddColumn(new TableColumn("DateTime").Centered());
        table.AddColumn(new TableColumn("Value").Centered());
        table.AddColumn(new TableColumn("Balance").Centered());
        foreach (History row in result)
        {
            table.AddRow(
                row.OperationType.ToString(),
                row.DateTime.ToString(CultureInfo.InvariantCulture),
                row.Value.ToString(CultureInfo.InvariantCulture),
                row.Balance.ToString(CultureInfo.InvariantCulture));
            AnsiConsole.WriteLine();
        }

        AnsiConsole.Write(table);

        AnsiConsole.Ask<string>("Ok");
    }
}