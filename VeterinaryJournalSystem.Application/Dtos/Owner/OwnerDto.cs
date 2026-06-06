namespace VeterinaryJournalSystem.Application.Dtos.Owner;

public class OwnerDto
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PersonalNumber { get; set; } = string.Empty;
    public string? Comment { get; set; }
}