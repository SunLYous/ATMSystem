namespace ATMSystem.Application.Models.Histories;
public record History(long Id, DateTime DateTime, OperationType OperationType, long Value, long Balance);