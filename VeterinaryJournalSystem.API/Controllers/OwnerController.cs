using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinaryJournalSystem.API.Dtos.Owner;
using VeterinaryJournalSystem.API.Services.Owners;

namespace VeterinaryJournalSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OwnersController : ControllerBase
{
    private readonly IOwnerService _ownerService;

    public OwnersController(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOwners()
    {
        var owners = await _ownerService.GetAllOwnersAsync();
        return Ok(owners);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOwnerById(string id)
    {
        var owner = await _ownerService.GetOwnerByIdAsync(id);

        if (owner == null)
            return NotFound("Owner not found.");

        return Ok(owner);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchOwnerByPersonalNumber([FromQuery] string personalNumber)
    {
        var owner = await _ownerService.SearchOwnerByPersonalNumberAsync(personalNumber);

        if (owner == null)
            return NotFound("Owner not found.");

        return Ok(owner);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOwner(CreateOwnerDto dto)
    {
        var owner = await _ownerService.CreateOwnerAsync(dto);
        return Ok(owner);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOwner(string id, UpdateOwnerDto dto)
    {
        var owner = await _ownerService.UpdateOwnerAsync(id, dto);

        if (owner == null)
            return NotFound("Owner not found.");

        return Ok(owner);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOwner(string id)
    {
        var deleted = await _ownerService.DeleteOwnerAsync(id);

        if (!deleted)
            return NotFound("Owner not found.");

        return NoContent();
    }
}