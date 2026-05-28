using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryJournalSystem.API.Dtos.Pet;
using VeterinaryJournalSystem.API.Models;

namespace VeterinaryJournalSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PetsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PetsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePet(CreatePetDto dto)
    {
        var owner = await _context.Owners.FindAsync(dto.OwnerId);

        if (owner == null)
        {
            return NotFound("Owner not found.");
        }

        var pet = new Pet
        {
            Name = dto.Name,
            Species = dto.Species,
            Breed = dto.Breed,
            DateOfBirth = dto.DateOfBirth,
            IsInsured = dto.IsInsured,
            OwnerId = dto.OwnerId
        };

        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            pet.Id,
            pet.Name,
            pet.Species,
            pet.Breed,
            pet.IsInsured,
            pet.DateOfBirth,
            pet.OwnerId
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPetById(string id)
    {
        var pet = await _context.Pets
            .Include(p => p.Visits)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pet == null)
            return NotFound("Pet not found.");

        return Ok(pet);
    }

    [HttpGet("by-owner/{ownerId}")]
    public async Task<IActionResult> GetPetsByOwnerId(string ownerId)
    {
        var pets = await _context.Pets
            .Where(p => p.OwnerId == ownerId)
            .ToListAsync();

        return Ok(pets);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePet(string id, UpdatePetDto dto)
    {
        var pet = await _context.Pets.FindAsync(id);

        if (pet == null)
            return NotFound("Pet not found.");

        pet.Name = dto.Name;
        pet.Species = dto.Species;
        pet.Breed = dto.Breed;
        pet.IsInsured = dto.IsInsured;
        pet.DateOfBirth = dto.DateOfBirth;
   

        await _context.SaveChangesAsync();

        return Ok(pet);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePet(string id)
    {
        var pet = await _context.Pets.FindAsync(id);

        if (pet == null)
            return NotFound("Pet not found.");

        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}