using ATMSystem.Application.Models;

namespace ATMSystem.Application.Contracts.UsersMode;

public interface ICurrentModeService
{
    UserMode? UserMode { get; }
}