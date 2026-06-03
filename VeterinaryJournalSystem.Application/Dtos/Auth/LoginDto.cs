namespace VeterinaryJournalSystem.Application.Dtos.Auth
{
    public class LoginDto
    {
        public string StaffCode { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
