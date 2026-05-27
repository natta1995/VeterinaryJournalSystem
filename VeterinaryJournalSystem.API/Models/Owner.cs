using VeterinaryJournalSystem.API.Models;

namespace VeterinaryJournalSystem.API.Models
{
    public class Owner
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string PersonalNumber { get; set; } = string.Empty;

        public string? Comment { get; set; }

        public List<Pet> Pets { get; set; } = new();
    }
}

