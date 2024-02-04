using ATMSystem.Application.Contracts.Accounts;
using ATMSystem.Application.Models.Accounts;

namespace ATMSystem.Application.Accounts;

public class CurrentAccountService : ICurrentAccountService
{
    public Account? Account { get; set; }
}