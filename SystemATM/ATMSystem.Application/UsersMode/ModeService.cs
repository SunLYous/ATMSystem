using ATMSystem.Application.Abstractions.Repositories;
using ATMSystem.Application.Accounts;
using ATMSystem.Application.Admins;
using ATMSystem.Application.Contracts.UsersMode;
using ATMSystem.Application.Models;
using ATMSystem.Application.Models.Accounts;
using ATMSystem.Application.Models.Admins;

namespace ATMSystem.Application.UsersMode;

public class ModeService : IModeService
{
    private readonly CurrentModeService _currentModeService;
    private readonly IAccountRepository _accountRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly CurrentAccountService _accountService;
    private readonly CurrentAdminService _adminService;

    public ModeService(
        CurrentModeService currentModeService,
        IAccountRepository accountRepository,
        CurrentAccountService accountService,
        CurrentAdminService adminService,
        IAdminRepository adminRepository)
    {
        _currentModeService = currentModeService;
        _accountRepository = accountRepository;
        _accountService = accountService;
        _adminService = adminService;
        _adminRepository = adminRepository;
    }

    public LoginResult LoginAdmin(long password)
    {
        Task<Admin?> admin = _adminRepository.CheckPassword(password);
        if (admin.Result is null)
        {
            return new LoginResult.NotFound();
        }

        if (admin.Result.Password != password)
        {
            return new LoginResult.InvalidPin();
        }

        _adminService.Admin = admin.Result;
        _currentModeService.UserMode = UserMode.Admin;
        return new LoginResult.Success();
    }

    public LoginResult LoginUser(long accountNumber, long pin)
    {
        Task<Account?> account = _accountRepository.FindAccountByAccountNumber(accountNumber);
        if (account.Result is null)
        {
            return new LoginResult.NotFound();
        }

        if (account.Result.Pin != pin)
        {
            return new LoginResult.InvalidPin();
        }

        _accountService.Account = account.Result;
        _currentModeService.UserMode = UserMode.Account;
        return new LoginResult.Success();
    }
}