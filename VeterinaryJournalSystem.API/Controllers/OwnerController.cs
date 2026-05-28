using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryJournalSystem.API;
using VeterinaryJournalSystem.API.Dtos.Owner;
using VeterinaryJournalSystem.API.Models;


namespace VeterinaryJournalSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OwnersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OwnersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOwner(CreateOwnerDto dto)
    {
        var owner = new Owner
        {
            FullName = dto.FullName,
            PhoneNumber = dto.PhoneNumber,
            PersonalNumber = dto.PersonalNumber,
            Comment = dto.Comment
        };

        _context.Owners.Add(owner);
        await _context.SaveChangesAsync();

        return Ok(owner);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOwners()
    {
        var owners = await _context.Owners.ToListAsync();

        return Ok(owners);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchOwnerByPersonalNumber([FromQuery] string personalNumber)
    {
        var owner = await _context.Owners
            .FirstOrDefaultAsync(o => o.PersonalNumber == personalNumber);

        if (owner == null)
        {
            return NotFound("Owner not found.");
        }

        return Ok(owner);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOwnerById(string id)
    {
        var owner = await _context.Owners.FindAsync(id);

        if (owner == null)
        {
            return NotFound("Owner not found.");
        }

        return Ok(owner);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOwner(string id, UpdateOwnerDto dto)
    {
        var owner = await _context.Owners.FindAsync(id);

        if (owner == null)
        {
            return NotFound("Owner not found.");
        }

        owner.FullName = dto.FullName;
        owner.PhoneNumber = dto.PhoneNumber;
        owner.PersonalNumber = dto.PersonalNumber;
        owner.Comment = dto.Comment;

        await _context.SaveChangesAsync();

        return Ok(owner);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOwner(string id)
    {
        var owner = await _context.Owners.FindAsync(id);

        if (owner == null)
        {
            return NotFound("Owner not found.");
        }

        _context.Owners.Remove(owner);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}