namespace ATMSystem.Application.Contracts.Accounts;

public interface IAccountService
{
    OperationResult Balance();
    OperationResult WithdrawMoney(long value);
    OperationResult TopAccount(long value);
}