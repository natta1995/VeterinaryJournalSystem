using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryJournalSystem.API.Dtos.Pet;
using VeterinaryJournalSystem.Domain.Entities;
using VeterinaryJournalSystem.API.Repositories;
using VeterinaryJournalSystem.API.Services.Pets;

namespace VeterinaryJournalSystem.Tests.Services
{
    public class PetServiceTest
    {
        [Fact]
        public async Task CreatePetAsync_ShouldThrowException_WhenOwnerDoesNotExist()
        {
            // Arrange
            var petRepository = Substitute.For<IGenericRepository<Pet>>();
            var ownerRepository = Substitute.For<IGenericRepository<Owner>>();

            var service = new PetService(petRepository, ownerRepository);

            var dto = new CreatePetDto
            {
                Name = "Morris",
                Species = "Cat",
                Breed = "Domestic Shorthair",
                IsInsured = true,
                DateOfBirth = new DateTime(2020, 5, 12),
                OwnerId = Guid.NewGuid().ToString()
            };

            ownerRepository.GetByIdAsync(dto.OwnerId)
                .Returns((Owner?)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                service.CreatePetAsync(dto));

            await petRepository.DidNotReceive().AddAsync(Arg.Any<Pet>());
            await petRepository.DidNotReceive().SaveChangesAsync();
        }

        [Fact]
        public async Task CreatePetAsync_ShouldCreatePet_WhenOwnerExists()
        {
            // Arrange
            var petRepository = Substitute.For<IGenericRepository<Pet>>();
            var ownerRepository = Substitute.For<IGenericRepository<Owner>>();

            var service = new PetService(petRepository, ownerRepository);

            var ownerId = Guid.NewGuid().ToString();

            var owner = new Owner
            {
                Id = ownerId,
                FullName = "Karin Andersson"
            };

            var dto = new CreatePetDto
            {
                Name = "Morris",
                Species = "Cat",
                Breed = "Domestic Shorthair",
                IsInsured = true,
                DateOfBirth = new DateTime(2020, 5, 12),
                OwnerId = ownerId
            };

            ownerRepository.GetByIdAsync(ownerId)
                .Returns(owner);

            // Act
            var result = await service.CreatePetAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Species, result.Species);
            Assert.Equal(dto.Breed, result.Breed);
            Assert.Equal(dto.OwnerId, result.OwnerId);

            await petRepository.Received(1).AddAsync(Arg.Any<Pet>());
            await petRepository.Received(1).SaveChangesAsync();
        }
    }
}
