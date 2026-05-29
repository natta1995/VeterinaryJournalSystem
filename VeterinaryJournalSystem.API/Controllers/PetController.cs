using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinaryJournalSystem.API.Dtos.Pet;
using VeterinaryJournalSystem.API.Services;

namespace VeterinaryJournalSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PetsController : ControllerBase
{
    private readonly IPetService _petService;

    public PetsController(IPetService petService)
    {
        _petService = petService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePet(CreatePetDto dto)
    {
        try
        {
            var pet = await _petService.CreatePetAsync(dto);
            return Ok(pet);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPetById(string id)
    {
        var pet = await _petService.GetPetByIdAsync(id);

        if (pet == null)
        {
            return NotFound("Pet not found.");
        }

        return Ok(pet);
    }

    [HttpGet("by-owner/{ownerId}")]
    public async Task<IActionResult> GetPetsByOwnerId(string ownerId)
    {
        var pets = await _petService.GetPetsByOwnerIdAsync(ownerId);

        return Ok(pets);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePet(string id, UpdatePetDto dto)
    {
        var pet = await _petService.UpdatePetAsync(id, dto);

        if (pet == null)
        {
            return NotFound("Pet not found.");
        }

        return Ok(pet);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePet(string id)
    {
        var deleted = await _petService.DeletePetAsync(id);

        if (!deleted)
        {
            return NotFound("Pet not found.");
        }

        return NoContent();
    }
}