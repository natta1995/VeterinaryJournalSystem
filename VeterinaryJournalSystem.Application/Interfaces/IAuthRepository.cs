using VeterinaryJournalSystem.Domain.Entities;

namespace VeterinaryJournalSystem.Application.Interfaces;

public interface IAuthRepository
{
    Task<bool> StaffCodeExistsAsync(string staffCode);

    Task<StaffUser?> GetUserByStaffCodeAsync(string staffCode);

    Task<(bool Succeeded, IEnumerable<string> Errors)> CreateUserAsync(
        StaffUser user,
        string password);

    Task<bool> CheckPasswordAsync(
        StaffUser user,
        string password);

    Task<IList<string>> GetRolesAsync(
        StaffUser user);
}
