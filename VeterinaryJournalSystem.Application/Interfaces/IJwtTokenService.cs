using VeterinaryJournalSystem.Domain.Entities;

namespace VeterinaryJournalSystem.Application.Interfaces;

public interface IJwtTokenService
{
    string CreateToken(StaffUser user, IList<string> roles);
}
