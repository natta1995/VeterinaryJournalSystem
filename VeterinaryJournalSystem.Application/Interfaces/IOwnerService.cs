using VeterinaryJournalSystem.Application.Dtos.Owner;
using VeterinaryJournalSystem.Domain.Entities;

namespace VeterinaryJournalSystem.Application.Interfaces;

public interface IOwnerService
{
    Task<List<OwnerDto>> GetAllOwnersAsync();

    Task<OwnerDto?> GetOwnerByIdAsync(string id);

    Task<OwnerDto?> SearchOwnerByPersonalNumberAsync(string personalNumber);

    Task<Owner> CreateOwnerAsync(CreateOwnerDto dto);

    Task<Owner?> UpdateOwnerAsync(string id, UpdateOwnerDto dto);

    Task<bool> DeleteOwnerAsync(string id);
}
