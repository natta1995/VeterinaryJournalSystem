namespace VeterinaryJournalSystem.Application.Dtos.Pet
{

        public class UpdatePetDto
        {
            public string Name { get; set; } = string.Empty;
            public string Species { get; set; } = string.Empty;
            public string Breed { get; set; } = string.Empty;
            public bool IsInsured { get; set; }
            public DateTime DateOfBirth { get; set; }

        
            }
}
