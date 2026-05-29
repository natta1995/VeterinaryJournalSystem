using VeterinaryJournalSystem.API.Dtos.Owner;
using VeterinaryJournalSystem.API.Models;

namespace VeterinaryJournalSystem.API.Services.Owner;

public interface IOwnerService
{
    Task<List<Owner>> GetAllOwnersAsync();

    Task<Owner?> GetOwnerByIdAsync(string id);

    Task<Owner?> SearchOwnerByPersonalNumberAsync(string personalNumber);

    Task<Owner> CreateOwnerAsync(CreateOwnerDto dto);

    Task<Owner?> UpdateOwnerAsync(string id, UpdateOwnerDto dto);

    Task<bool> DeleteOwnerAsync(string id);
}
