using VeterinaryJournalSystem.Application.Dtos.Visit;
using VeterinaryJournalSystem.Domain.Entities;


namespace VeterinaryJournalSystem.Application.Interfaces;

public interface IVisitService
{
    Task<Visit> CreateVisitAsync(CreateVisitDto dto);

    Task<VisitDto?> GetVisitByIdAsync(string id); 

    Task<List<VisitDto>> GetVisitsByPetIdAsync(string petId); 

    Task<Visit?> UpdateVisitAsync(string id, UpdateVisitDto dto);
}
