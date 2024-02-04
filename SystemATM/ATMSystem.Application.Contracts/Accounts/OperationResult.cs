using ATMSystem.Application.Models.Moneys;

namespace ATMSystem.Application.Contracts.Accounts;

public record OperationResult
{
    private OperationResult() { }

    public sealed record Success(Money Money) : OperationResult;

    public sealed record Loss : OperationResult;
}