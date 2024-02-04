using ATMSystem.Application.Models.Accounts;

namespace ATMSystem.Application.Contracts.Accounts;

public interface ICurrentAccountService
{
    Account? Account { get; }
}