using VeterinaryJournalSystem.Domain.Entities;

namespace VeterinaryJournalSystem.API.Dtos.Visit;

public class UpdateVisitDto
{
    public DateTime ScheduledAt { get; set; }

    public string ReasonForVisit { get; set; } = string.Empty;

    public string? Symptoms { get; set; }

    public string? Examination { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public string? VeterinarianNotes { get; set; }

    public VisitStatus Status { get; set; }
}
