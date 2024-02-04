namespace ATMSystem.Application.Contracts.UsersMode;

public interface IModeService
{
    LoginResult LoginAdmin(long password);

    LoginResult LoginUser(long accountNumber, long pin);
}