using ATMSystem.Application.Abstractions.Repositories;
using ATMSystem.Application.Contracts.Accounts;
using ATMSystem.Application.Contracts.Histories;
using ATMSystem.Application.Models.Histories;
using ATMSystem.Application.Models.Moneys;

namespace ATMSystem.Application.Accounts;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;
    private readonly CurrentAccountService _accountService;
    private readonly IHistoryService _historyService;

    public AccountService(
        IAccountRepository repository,
        CurrentAccountService accountService,
        IHistoryService historyService)
    {
        _repository = repository;
        _accountService = accountService;
        _historyService = historyService;
    }

    public OperationResult Balance()
    {
        if (_accountService.Account == null) return new OperationResult.Loss();
        Task<Money?> money = _repository.Balance(_accountService.Account.AccountNumber);

        if (money.Result is null)
        {
            return new OperationResult.Loss();
        }

        return new OperationResult.Success(money.Result);
    }

    public OperationResult WithdrawMoney(long value)
    {
        if (_accountService.Account == null)
            return new OperationResult.Loss();
        OperationResult result = _repository.WithdrawMoney(
            _accountService.Account.AccountNumber,
            new Money(value)).Result;
        if (result is OperationResult.Success success)
        {
            _historyService.AddOperation(
                _accountService.Account.AccountNumber,
                OperationType.WithdrawMoney,
                success.Money.Value,
                value);
        }

        return result;
    }

    public OperationResult TopAccount(long value)
    {
        if (_accountService.Account == null) return new OperationResult.Loss();
        Task<Money> money = _repository.TopAccount(
            _accountService.Account.AccountNumber,
            new Money(value));
        _historyService.AddOperation(
            _accountService.Account.AccountNumber,
            OperationType.TopAccount,
            money.Result.Value,
            value);
        return new OperationResult.Success(money.Result);
    }
}