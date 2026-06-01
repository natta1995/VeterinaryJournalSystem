using NSubstitute;
using VeterinaryJournalSystem.API.Dtos.Visit;
using VeterinaryJournalSystem.API.Models;
using VeterinaryJournalSystem.API.Repositories;
using VeterinaryJournalSystem.API.Services;
using VeterinaryJournalSystem.Models;

namespace VeterinaryJournalSystem.Tests.Services
{
    public class VisitServiceTest
    {
        [Fact]
        public async Task CreateVisitAsync_ShouldCreateVisit_WhenPetExists()
        {
            // Arrange
            var visitRepository = Substitute.For<IGenericRepository<Visit>>();
            var petRepository = Substitute.For<IGenericRepository<Pet>>();

            var service = new VisitService(visitRepository, petRepository);

            var petId = Guid.NewGuid().ToString();

            var pet = new Pet
            {
                Id = petId,
                Name = "Morris"
            };

            var dto = new CreateVisitDto
            {
                PetId = petId,
                ScheduledAt = new DateTime(2026, 6, 10, 14, 30, 0),
                ReasonForVisit = "Annual health check"
            };

            petRepository.GetByIdAsync(petId)
                .Returns(pet);

            // Act
            var result = await service.CreateVisitAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.PetId, result.PetId);
            Assert.Equal(dto.ScheduledAt, result.ScheduledAt);
            Assert.Equal(dto.ReasonForVisit, result.ReasonForVisit);
            Assert.Equal(VisitStatus.Scheduled, result.Status);

            await visitRepository.Received(1).AddAsync(Arg.Any<Visit>());
            await visitRepository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task CreateVisitAsync_ShouldThrowException_WhenPetDoesNotExist()
        {
            // Arrange
            var visitRepository = Substitute.For<IGenericRepository<Visit>>();
            var petRepository = Substitute.For<IGenericRepository<Pet>>();

            var service = new VisitService(visitRepository, petRepository);

            var dto = new CreateVisitDto
            {
                PetId = Guid.NewGuid().ToString(),
                ScheduledAt = new DateTime(2026, 6, 10, 14, 30, 0),
                ReasonForVisit = "Annual health check"
            };

            petRepository.GetByIdAsync(dto.PetId)
                .Returns((Pet?)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                service.CreateVisitAsync(dto));

            await visitRepository.DidNotReceive().AddAsync(Arg.Any<Visit>());
            await visitRepository.DidNotReceive().SaveChangesAsync();
        }
    }
}
