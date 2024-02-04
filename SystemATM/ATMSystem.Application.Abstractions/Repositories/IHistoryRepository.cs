using ATMSystem.Application.Contracts.Histories;
using ATMSystem.Application.Models.Histories;

namespace ATMSystem.Application.Abstractions.Repositories;

public interface IHistoryRepository
{
    Task<IEnumerable<History>> GetHistories(long accountId);
    Task<CreateHistoryRowResult> AddHistory(long accountId, OperationType operationType, long balance, long value);
}