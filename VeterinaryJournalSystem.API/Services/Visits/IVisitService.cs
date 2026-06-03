using VeterinaryJournalSystem.API.Dtos.Visit;
using VeterinaryJournalSystem.Domain.Entities;


namespace VeterinaryJournalSystem.API.Services;

public interface IVisitService
{
    Task<Visit> CreateVisitAsync(CreateVisitDto dto);

    Task<Visit?> GetVisitByIdAsync(string id);

    Task<List<Visit>> GetVisitsByPetIdAsync(string petId);

    Task<Visit?> UpdateVisitAsync(string id, UpdateVisitDto dto);
}
