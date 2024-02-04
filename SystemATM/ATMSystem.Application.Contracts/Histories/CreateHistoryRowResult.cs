namespace ATMSystem.Application.Contracts.Histories;

public record CreateHistoryRowResult
{
    private CreateHistoryRowResult() { }

    public sealed record Success : CreateHistoryRowResult;

    public sealed record Loss : CreateHistoryRowResult;
}