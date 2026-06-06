using VeterinaryJournalSystem.Application.Dtos.Owner;
using VeterinaryJournalSystem.Domain.Entities;
using VeterinaryJournalSystem.Application.Interfaces;

namespace VeterinaryJournalSystem.Application.Services;

public class OwnerService : IOwnerService
{
    private readonly IGenericRepository<Owner> _ownerRepository;

    public OwnerService(IGenericRepository<Owner> ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    public async Task<List<OwnerDto>> GetAllOwnersAsync()
    {
        var owners = await _ownerRepository.GetAllAsync();

        return owners.Select(o => new OwnerDto
        {
            Id = o.Id,
            FullName = o.FullName,
            PhoneNumber = o.PhoneNumber,
            PersonalNumber = o.PersonalNumber,
            Comment = o.Comment
        }).ToList();
    }

    public async Task<OwnerDto?> GetOwnerByIdAsync(string id)
    {
        var owner = await _ownerRepository.GetByIdAsync(id);

        if (owner == null)
            return null;

        return new OwnerDto
        {
            Id = owner.Id,
            FullName = owner.FullName,
            PhoneNumber = owner.PhoneNumber,
            PersonalNumber = owner.PersonalNumber,
            Comment = owner.Comment
        };
    }

    public async Task<OwnerDto?> SearchOwnerByPersonalNumberAsync(string personalNumber)
    {
        var owners = await _ownerRepository.GetAllAsync();

        var owner = owners.FirstOrDefault(o => o.PersonalNumber == personalNumber);

        if (owner == null)
            return null;

        return new OwnerDto
        {
            Id = owner.Id,
            FullName = owner.FullName,
            PhoneNumber = owner.PhoneNumber,
            PersonalNumber = owner.PersonalNumber,
            Comment = owner.Comment
        };
    }

    public async Task<Owner> CreateOwnerAsync(CreateOwnerDto dto)
    {
        var personalNumber = dto.PersonalNumber.Replace("-", "");

        if (string.IsNullOrWhiteSpace(personalNumber))
        {
            throw new ArgumentException("Personal number is required.");
        }

        if (personalNumber.Length != 12)
        {
            throw new ArgumentException("Personal number must contain 12 digits.");
        }

        var owner = new Owner
        {
            FullName = dto.FullName,
            PhoneNumber = dto.PhoneNumber,
            PersonalNumber = dto.PersonalNumber,
            Comment = dto.Comment
        };

        await _ownerRepository.AddAsync(owner);
        await _ownerRepository.SaveChangesAsync();

        return owner;
    }

    public async Task<Owner?> UpdateOwnerAsync(string id, UpdateOwnerDto dto)
    {
        var owner = await _ownerRepository.GetByIdAsync(id);

        if (owner == null)
        {
            return null;
        }

        owner.FullName = dto.FullName;
        owner.PhoneNumber = dto.PhoneNumber;
        owner.PersonalNumber = dto.PersonalNumber;
        owner.Comment = dto.Comment;

        _ownerRepository.Update(owner);
        await _ownerRepository.SaveChangesAsync();

        return owner;
    }

    public async Task<bool> DeleteOwnerAsync(string id)
    {
        var owner = await _ownerRepository.GetByIdAsync(id);

        if (owner == null)
        {
            return false;
        }

        _ownerRepository.Delete(owner);
        await _ownerRepository.SaveChangesAsync();

        return true;
    }
}