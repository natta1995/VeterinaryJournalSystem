namespace VeterinaryJournalSystem.API.Dtos.Owner
{
    public class CreateOwnerDto
    {
        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string PersonalNumber { get; set; } = string.Empty;

        public string? Comment { get; set; }
    }
}
