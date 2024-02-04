using ATMSystem.Application.Models.Accounts;
using ATMSystem.Application.Models.Admins;

namespace ATMSystem.Application.Abstractions.Repositories;

public interface IAdminRepository
{
    Task<Admin?> CheckPassword(long password);
    Task ChangeAdminPassword(long id, long newPassword);
    Task<Account?> AddAccount(long pin);
}