
using VeterinaryJournalSystem.Domain.Entities;

namespace VeterinaryJournalSystem.Domain.Entities
{
    public class Pet
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public bool IsInsured { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string OwnerId { get; set; } = string.Empty;

        public Owner Owner { get; set; }

        public List<Visit> Visits { get; set; } = new();
        
    }
}
