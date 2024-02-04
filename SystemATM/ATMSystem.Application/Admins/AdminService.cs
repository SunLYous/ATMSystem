using ATMSystem.Application.Abstractions.Repositories;
using ATMSystem.Application.Contracts.Admins;
using ATMSystem.Application.Models.Accounts;
using ATMSystem.Application.Models.Admins;

namespace ATMSystem.Application.Admins;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _repository;

    public AdminService(IAdminRepository repository)
    {
        _repository = repository;
    }

    public ChangePasswordResult ChangePassword(long password, long newPassword)
    {
        Task<Admin?> admin = _repository.CheckPassword(password);
        if (admin.Result is null)
        {
            return new ChangePasswordResult.InvalidPassword();
        }

        _repository.ChangeAdminPassword(admin.Id, newPassword);
        return new ChangePasswordResult.Success();
    }

    public RegistrationResult RegistrationAccount(long pin)
    {
        Task<Account?> account = _repository.AddAccount(pin);
        if (account.Result is null)
        {
            return new RegistrationResult.Loss();
        }

        return new RegistrationResult.Success(account.Result.AccountNumber, account.Result.Pin);
    }
}