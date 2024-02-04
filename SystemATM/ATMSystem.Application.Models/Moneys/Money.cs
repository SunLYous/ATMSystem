namespace ATMSystem.Application.Models.Moneys;

public record Money
{
    public Money(long value)
    {
        if (value < 0)
        {
            throw new ArgumentException("value less than zero", nameof(value));
        }

        Value = value;
    }

    public long Value { get; private set; }
}