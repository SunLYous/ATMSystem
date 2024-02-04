namespace ATMSystem.Application.Contracts.Admins;

public record RegistrationResult
{
    private RegistrationResult() { }

    public sealed record Success(long AccountNumber, long Pin) : RegistrationResult;

    public sealed record Loss : RegistrationResult;
}