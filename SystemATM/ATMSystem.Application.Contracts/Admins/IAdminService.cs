namespace ATMSystem.Application.Contracts.Admins;

public interface IAdminService
{
    ChangePasswordResult ChangePassword(long password, long newPassword);
    RegistrationResult RegistrationAccount(long pin);
}