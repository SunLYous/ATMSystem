using ATMSystem.Application.Contracts.UsersMode;
using ATMSystem.Application.Models;

namespace ATMSystem.Application.UsersMode;

public class CurrentModeService : ICurrentModeService
{
    public UserMode? UserMode { get; set; }
}