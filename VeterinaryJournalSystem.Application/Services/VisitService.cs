using VeterinaryJournalSystem.Application.Dtos.Visit;
using VeterinaryJournalSystem.Domain.Entities;
using VeterinaryJournalSystem.Domain.Enums;
using VeterinaryJournalSystem.Application.Interfaces;



namespace VeterinaryJournalSystem.Application.Services;

public class VisitService : IVisitService
{
    private readonly IGenericRepository<Visit> _visitRepository;
    private readonly IGenericRepository<Pet> _petRepository;

    public VisitService(
        IGenericRepository<Visit> visitRepository,
        IGenericRepository<Pet> petRepository)
    {
        _visitRepository = visitRepository;
        _petRepository = petRepository;
    }

    public async Task<Visit> CreateVisitAsync(CreateVisitDto dto)
    {
        var pet = await _petRepository.GetByIdAsync(dto.PetId);

        if (pet == null)
        {
            throw new Exception("Pet not found.");
        }

        var visit = new Visit
        {
            PetId = dto.PetId,
            ScheduledAt = dto.ScheduledAt,
            ReasonForVisit = dto.ReasonForVisit,
            Status = VisitStatus.Scheduled
        };

        await _visitRepository.AddAsync(visit);
        await _visitRepository.SaveChangesAsync();

        return visit;
    }

    public async Task<Visit?> GetVisitByIdAsync(string id)
    {
        return await _visitRepository.GetByIdAsync(id);
    }

    public async Task<List<VisitDto>> GetVisitsByPetIdAsync(string petId)
    {
        var visits = await _visitRepository.GetAllAsync();

        return visits
            .Where(v => v.PetId == petId)
            .Select(v => new VisitDto
            {
                Id = v.Id,
                PetId = v.PetId,
                ScheduledAt = v.ScheduledAt,
                ReasonForVisit = v.ReasonForVisit,
                Symptoms = v.Symptoms,
                Examination = v.Examination,
                Diagnosis = v.Diagnosis,
                Treatment = v.Treatment,
                VeterinarianNotes = v.VeterinarianNotes,
                Status = v.Status
            })
            .ToList();
    }

    public async Task<Visit?> UpdateVisitAsync(string id, UpdateVisitDto dto)
    {
        var visit = await _visitRepository.GetByIdAsync(id);

        if (visit == null)
        {
            return null;
        }

        visit.ScheduledAt = dto.ScheduledAt;
        visit.ReasonForVisit = dto.ReasonForVisit;
        visit.Symptoms = dto.Symptoms;
        visit.Examination = dto.Examination;
        visit.Diagnosis = dto.Diagnosis;
        visit.Treatment = dto.Treatment;
        visit.VeterinarianNotes = dto.VeterinarianNotes;
        visit.Status = dto.Status;

        _visitRepository.Update(visit);
        await _visitRepository.SaveChangesAsync();

        return visit;
    }
}