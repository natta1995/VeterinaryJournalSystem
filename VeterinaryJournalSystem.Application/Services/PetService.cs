using VeterinaryJournalSystem.Application.Dtos.Pet;
using VeterinaryJournalSystem.Domain.Entities;
using VeterinaryJournalSystem.Application.Interfaces;

namespace VeterinaryJournalSystem.Application.Services;

public class PetService : IPetService
{
    private readonly IGenericRepository<Pet> _petRepository;
    private readonly IGenericRepository<Owner> _ownerRepository;

    public PetService(
        IGenericRepository<Pet> petRepository,
        IGenericRepository<Owner> ownerRepository)
    {
        _petRepository = petRepository;
        _ownerRepository = ownerRepository;
    }

    public async Task<List<PetDto>> GetPetsByOwnerIdAsync(string ownerId)
    {
        var pets = await _petRepository.GetAllAsync();

        return pets
            .Where(p => p.OwnerId == ownerId)
            .Select(p => new PetDto
            {
                Id = p.Id,
                Name = p.Name,
                Species = p.Species,
                Breed = p.Breed,
                IsInsured = p.IsInsured,
                DateOfBirth = p.DateOfBirth,
                OwnerId = p.OwnerId
            })
            .ToList();
    }

    public async Task<PetDto?> GetPetByIdAsync(string id)
    {
        var pet = await _petRepository.GetByIdAsync(id);

        if (pet == null)
            return null;

        return new PetDto
        {
            Id = pet.Id,
            Name = pet.Name,
            Species = pet.Species,
            Breed = pet.Breed,
            IsInsured = pet.IsInsured,
            DateOfBirth = pet.DateOfBirth,
            OwnerId = pet.OwnerId
        };
    }
    public async Task<Pet> CreatePetAsync(CreatePetDto dto)
    {
        var owner = await _ownerRepository.GetByIdAsync(dto.OwnerId);

        if (owner == null)
        {
            throw new Exception("Owner not found.");
        }

        var pet = new Pet
        {
            Name = dto.Name,
            Species = dto.Species,
            Breed = dto.Breed,
            IsInsured = dto.IsInsured,
            DateOfBirth = dto.DateOfBirth,
            OwnerId = dto.OwnerId
        };

        await _petRepository.AddAsync(pet);
        await _petRepository.SaveChangesAsync();

        return pet;
    }

    public async Task<Pet?> UpdatePetAsync(string id, UpdatePetDto dto)
    {
        var pet = await _petRepository.GetByIdAsync(id);

        if (pet == null)
        {
            return null;
        }

        pet.Name = dto.Name;
        pet.Species = dto.Species;
        pet.Breed = dto.Breed;
        pet.IsInsured = dto.IsInsured;
        pet.DateOfBirth = dto.DateOfBirth;


        _petRepository.Update(pet);
        await _petRepository.SaveChangesAsync();

        return pet;
    }

    public async Task<bool> DeletePetAsync(string id)
    {
        var pet = await _petRepository.GetByIdAsync(id);

        if (pet == null)
        {
            return false;
        }

        _petRepository.Delete(pet);
        await _petRepository.SaveChangesAsync();

        return true;
    }



}

