using ATMSystem.Application.Contracts.Accounts;
using ATMSystem.Application.Models.Accounts;
using ATMSystem.Application.Models.Moneys;

namespace ATMSystem.Application.Abstractions.Repositories;

public interface IAccountRepository
{
    Task<Account?> FindAccountByAccountNumber(long accountNumber);
    Task<Money?> Balance(long id);
    Task<OperationResult> WithdrawMoney(long id, Money updateMoney);
    Task<Money> TopAccount(long id, Money updateMoney);
}