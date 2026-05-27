using VeterinaryJournalSystem.API.Models;

namespace VeterinaryJournalSystem.API.Models
{
    public class Pet
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public string OwnerId { get; set; } = string.Empty;

        public Owner Owner { get; set; }

    }
}
