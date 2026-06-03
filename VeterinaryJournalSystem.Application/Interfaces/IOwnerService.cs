using VeterinaryJournalSystem.Application.Dtos.Owner;
using VeterinaryJournalSystem.Domain.Entities;

namespace VeterinaryJournalSystem.Application.Services.Owners;

public interface IOwnerService
{
    Task<List<Owner>> GetAllOwnersAsync();

    Task<Owner?> GetOwnerByIdAsync(string id);

    Task<Owner?> SearchOwnerByPersonalNumberAsync(string personalNumber);

    Task<Owner> CreateOwnerAsync(CreateOwnerDto dto);

    Task<Owner?> UpdateOwnerAsync(string id, UpdateOwnerDto dto);

    Task<bool> DeleteOwnerAsync(string id);
}
