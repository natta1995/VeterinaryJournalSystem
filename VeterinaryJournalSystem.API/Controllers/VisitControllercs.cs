using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VeterinaryJournalSystem.Application.Dtos.Visit;
using VeterinaryJournalSystem.Application.Services.Visits;

namespace VeterinaryJournalSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VisitsController : ControllerBase
{
    private readonly IVisitService _visitService;

    public VisitsController(IVisitService visitService)
    {
        _visitService = visitService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateVisit(CreateVisitDto dto)
    {
        try
        {
            var visit = await _visitService.CreateVisitAsync(dto);
            return Ok(visit);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVisitById(string id)
    {
        var visit = await _visitService.GetVisitByIdAsync(id);

        if (visit == null)
        {
            return NotFound("Visit not found.");
        }

        return Ok(visit);
    }

    [HttpGet("by-pet/{petId}")]
    public async Task<IActionResult> GetVisitsByPetId(string petId)
    {
        var visits = await _visitService.GetVisitsByPetIdAsync(petId);

        return Ok(visits);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVisit(string id, UpdateVisitDto dto)
    {
        var visit = await _visitService.UpdateVisitAsync(id, dto);

        if (visit == null)
        {
            return NotFound("Visit not found.");
        }

        return Ok(visit);
    }
}