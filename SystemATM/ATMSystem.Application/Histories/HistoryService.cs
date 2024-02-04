using ATMSystem.Application.Abstractions.Repositories;
using ATMSystem.Application.Accounts;
using ATMSystem.Application.Contracts.Histories;
using ATMSystem.Application.Models.Histories;

namespace ATMSystem.Application.Histories;

public class HistoryService : IHistoryService
{
    private readonly IHistoryRepository _repository;
    private readonly CurrentAccountService _currentAccountService;

    public HistoryService(IHistoryRepository repository, CurrentAccountService currentAccountService)
    {
        _repository = repository;
        _currentAccountService = currentAccountService;
    }

    public IEnumerable<History> GetHistories()
    {
        if (_currentAccountService.Account is null)
            throw new ArgumentNullException($"Account id don`t found");
        return _repository.GetHistories(_currentAccountService.Account.AccountNumber).Result;
    }

    public CreateHistoryRowResult AddOperation(long accountId, OperationType operationType, long balance, long value)
    {
         return _repository.AddHistory(accountId, operationType, balance, value).Result;
    }
}