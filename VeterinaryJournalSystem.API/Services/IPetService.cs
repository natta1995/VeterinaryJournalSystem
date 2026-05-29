
using VeterinaryJournalSystem.API.Dtos.Pet;
using VeterinaryJournalSystem.API.Models;

namespace VeterinaryJournalSystem.API.Services;

public interface IPetService
{
    Task<List<Pet>> GetPetsByOwnerIdAsync(string ownerId);

    Task<Pet?> GetPetByIdAsync(string id);

    Task<Pet> CreatePetAsync(CreatePetDto dto);

    Task<Pet?> UpdatePetAsync(string id, UpdatePetDto dto);

    Task<bool> DeletePetAsync(string id);
}