using VeterinaryJournalSystem.Domain.Enums;

namespace VeterinaryJournalSystem.Domain.Entities;

public class Visit
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public DateTime ScheduledAt { get; set; }

    public string ReasonForVisit { get; set; } = string.Empty;

    public string? Symptoms { get; set; }

    public string? Examination { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public string? VeterinarianNotes { get; set; }

    public VisitStatus Status { get; set; } = VisitStatus.Scheduled;

    public string PetId { get; set; } = string.Empty;

    public Pet Pet { get; set; }

    public string? VeterinarianId { get; set; }

    public StaffUser? Veterinarian { get; set; }

}