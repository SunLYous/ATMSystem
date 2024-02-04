namespace ATMSystem.Application.Contracts.Admins;

public abstract record ChangePasswordResult
{
    private ChangePasswordResult() { }
    public sealed record Success : ChangePasswordResult;

    public sealed record InvalidPassword : ChangePasswordResult;
    public sealed record Loss : ChangePasswordResult;
}