using VeterinaryJournalSystem.Application.Dtos.Visit;
using VeterinaryJournalSystem.Domain.Entities;


namespace VeterinaryJournalSystem.Application.Interfaces;

public interface IVisitService
{
    Task<Visit> CreateVisitAsync(CreateVisitDto dto);

    Task<Visit?> GetVisitByIdAsync(string id); // här

    Task<List<VisitDto>> GetVisitsByPetIdAsync(string petId); // här

    Task<Visit?> UpdateVisitAsync(string id, UpdateVisitDto dto);
}
