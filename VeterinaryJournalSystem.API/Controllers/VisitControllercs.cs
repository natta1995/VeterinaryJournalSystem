using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryJournalSystem.API.Dtos.Visit;
using VeterinaryJournalSystem.API.Models;
using VeterinaryJournalSystem.Models;

namespace VeterinaryJournalSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VisitsController : ControllerBase
{
    private readonly AppDbContext _context;

    public VisitsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateVisit(CreateVisitDto dto)
    {
        var pet = await _context.Pets.FindAsync(dto.PetId);

        if (pet == null)
        {
            return NotFound("Pet not found.");
        }

        var visit = new Visit
        {
            PetId = dto.PetId,
            ScheduledAt = dto.ScheduledAt,
            ReasonForVisit = dto.ReasonForVisit,
            Status = VisitStatus.Scheduled
        };

        _context.Visits.Add(visit);
        await _context.SaveChangesAsync();

        return Ok(visit);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVisitById(string id)
    {
        var visit = await _context.Visits
            .Include(v => v.Pet)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (visit == null)
        {
            return NotFound("Visit not found.");
        }

        return Ok(visit);
    }

    [HttpGet("by-pet/{petId}")]
    public async Task<IActionResult> GetVisitsByPetId(string petId)
    {
        var visits = await _context.Visits
            .Where(v => v.PetId == petId)
            .ToListAsync();

        return Ok(visits);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVisit(string id, UpdateVisitDto dto)
    {
        var visit = await _context.Visits.FindAsync(id);

        if (visit == null)
        {
            return NotFound("Visit not found.");
        }

        visit.ScheduledAt = dto.ScheduledAt;
        visit.ReasonForVisit = dto.ReasonForVisit;
        visit.Symptoms = dto.Symptoms;
        visit.Examination = dto.Examination;
        visit.Diagnosis = dto.Diagnosis;
        visit.Treatment = dto.Treatment;
        visit.VeterinarianNotes = dto.VeterinarianNotes;
        visit.Status = dto.Status;

        await _context.SaveChangesAsync();

        return Ok(visit);
    }
}