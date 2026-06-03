using VeterinaryJournalSystem.Application.Dtos.Pet;
using VeterinaryJournalSystem.Domain.Entities;

namespace VeterinaryJournalSystem.API.Services.Pets;

public interface IPetService
{
    Task<List<Pet>> GetPetsByOwnerIdAsync(string ownerId);

    Task<Pet?> GetPetByIdAsync(string id);

    Task<Pet> CreatePetAsync(CreatePetDto dto);

    Task<Pet?> UpdatePetAsync(string id, UpdatePetDto dto);

    Task<bool> DeletePetAsync(string id);
}