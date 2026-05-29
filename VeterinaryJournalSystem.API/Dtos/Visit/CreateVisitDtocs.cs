namespace VeterinaryJournalSystem.API.Dtos.Visit;

public class CreateVisitDto
{
    public string PetId { get; set; } = string.Empty;

    public DateTime ScheduledAt { get; set; }

    public string ReasonForVisit { get; set; } = string.Empty;
}