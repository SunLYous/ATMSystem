namespace ATMSystem.Application.Contracts.UsersMode;

public abstract record LoginResult
{
    private LoginResult() { }

    public sealed record Success : LoginResult;

    public sealed record NotFound : LoginResult;

    public sealed record InvalidPin : LoginResult;
}