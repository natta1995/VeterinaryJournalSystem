
using VeterinaryJournalSystem.Domain.Enums;

public class VisitDto
{
    public string Id { get; set; } = string.Empty;
    public string PetId { get; set; } = string.Empty;

    public DateTime ScheduledAt { get; set; }
    public string ReasonForVisit { get; set; } = string.Empty;

    public string? Symptoms { get; set; }
    public string? Examination { get; set; }
    public string? Diagnosis { get; set; }
    public string? Treatment { get; set; }
    public string? VeterinarianNotes { get; set; }

    public VisitStatus Status { get; set; }
}