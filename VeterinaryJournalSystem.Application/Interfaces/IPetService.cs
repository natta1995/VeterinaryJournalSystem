using VeterinaryJournalSystem.Application.Dtos.Pet;
using VeterinaryJournalSystem.Domain.Entities;

namespace VeterinaryJournalSystem.Application.Interfaces;

public interface IPetService
{
    Task<List<PetDto>> GetPetsByOwnerIdAsync(string ownerId);
    Task<PetDto?> GetPetByIdAsync(string id);

    Task<Pet> CreatePetAsync(CreatePetDto dto);

    Task<Pet?> UpdatePetAsync(string id, UpdatePetDto dto);

    Task<bool> DeletePetAsync(string id);
}