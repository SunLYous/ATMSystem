using ATMSystem.Application.Contracts.Admins;
using ATMSystem.Application.Models.Admins;

namespace ATMSystem.Application.Admins;

public class CurrentAdminService : ICurrentAdminService
{
    public Admin? Admin { get; set; }
}