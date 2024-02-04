using ATMSystem.Application.Models.Admins;

namespace ATMSystem.Application.Contracts.Admins;

public interface ICurrentAdminService
{
    Admin? Admin { get; }
}