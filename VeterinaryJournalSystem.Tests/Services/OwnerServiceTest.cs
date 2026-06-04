using NSubstitute;
using VeterinaryJournalSystem.Application.Dtos.Owner;
using VeterinaryJournalSystem.Domain.Entities;
using VeterinaryJournalSystem.Application.Interfaces;
using VeterinaryJournalSystem.Application.Services;


namespace VeterinaryJournalSystem.Tests.Services;

public class OwnerServiceTests
{
    // Happy Path-test

    // 
    [Fact]
    public async Task CreateOwnerAsync_ShouldCreateOwner_WhenDtoIsValid()
    {
        // Arrange
        var ownerRepository = Substitute.For<IGenericRepository<Owner>>(); // (Mockar) Skapar den fakade "databasen"  eller skapar ett fejk-repository med NSubstitute
        var service = new OwnerService(ownerRepository);

        var dto = new CreateOwnerDto
        {
            FullName = "Karin Andersson",
            PhoneNumber = "0701234567",
            PersonalNumber = "19850512-1234",
            Comment = "Prefers afternoon appointments"
        };

        // Act
        var result = await service.CreateOwnerAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dto.FullName, result.FullName);
        Assert.Equal(dto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(dto.PersonalNumber, result.PersonalNumber);

        await ownerRepository.Received(1).AddAsync(Arg.Any<Owner>()); // Kontrollerar att den bara körs en gång
        await ownerRepository.Received(1).SaveChangesAsync(); // Kontrollerar att den bara sparar en gång
    }

    [Fact]
    public async Task GetOwnerByIdAsync_ShouldReturnOwner_WhenOwnerExists()
    {
        // Arrange
        var ownerRepository = Substitute.For<IGenericRepository<Owner>>();
        var service = new OwnerService(ownerRepository);

        var owner = new Owner
        {
            Id = "1",
            FullName = "Karin Andersson"
        };

        ownerRepository.GetByIdAsync("1")
            .Returns(owner);

        // Act
        var result = await service.GetOwnerByIdAsync("1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Karin Andersson", result.FullName);
    }

    [Fact]
    public async Task CreateOwnerAsync_ShouldThrowArgumentException_WhenPersonalNumberHasTenDigits()
    {
        // Arrange
        var ownerRepository = Substitute.For<IGenericRepository<Owner>>();
        var service = new OwnerService(ownerRepository);

        var dto = new CreateOwnerDto
        {
            FullName = "Karin Andersson",
            PhoneNumber = "0701234567",
            PersonalNumber = "8505121234",
            Comment = "Test"
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.CreateOwnerAsync(dto));

        await ownerRepository.DidNotReceive().AddAsync(Arg.Any<Owner>());
        await ownerRepository.DidNotReceive().SaveChangesAsync();
    }
}