using VeterinaryJournalSystem.Application.Dtos.Pet;
using VeterinaryJournalSystem.Domain.Entities;

namespace VeterinaryJournalSystem.Application.Interfaces;

public interface IPetService
{
    Task<List<Pet>> GetPetsByOwnerIdAsync(string ownerId); // här

    Task<Pet?> GetPetByIdAsync(string id); // här

    Task<Pet> CreatePetAsync(CreatePetDto dto);

    Task<Pet?> UpdatePetAsync(string id, UpdatePetDto dto);

    Task<bool> DeletePetAsync(string id);
}