using ATMSystem.Application.Models.Histories;

namespace ATMSystem.Application.Contracts.Histories;

public interface IHistoryService
{
    IEnumerable<History> GetHistories();
    CreateHistoryRowResult AddOperation(long accountId, OperationType operationType, long balance, long value);
}