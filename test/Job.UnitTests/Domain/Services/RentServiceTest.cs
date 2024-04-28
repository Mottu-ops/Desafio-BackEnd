using Job.Commons.Domain.Commands.Rent;
using Job.Commons.Domain.Entities.Moto;
using Job.Commons.Domain.Entities.Rent;
using Job.Commons.Domain.Entities.User;
using Job.Domain.Entities.Moto;
using Job.Domain.Entities.Rent;
using Job.Domain.Entities.User;
using Job.Domain.Enums;

namespace Job.UnitTests.Domain.Services;

[Trait("Services", "RentService")]
public class RentServiceTest
{
    private readonly Mock<ILogger<RentService>> _logger = new();
    private readonly Mock<IRentRepository> _rentRepository = new();
    private readonly Mock<IMotoRepository> _motoRepository = new();
    private readonly Mock<IMotoboyRepository> _motoboyRepository = new();
    private readonly RentService _rentService;

    public RentServiceTest()
    {
        _rentService = new RentService(
            _logger.Object,
            _rentRepository.Object,
            _motoRepository.Object,
            _motoboyRepository.Object);
    }

    #region CreateRentAsync

    [Fact]
    public async Task CreateRentAsync_WhenCommandIsValid_ShouldCreateRent()
    {
        // Arrange
        var command = CreateRentCommandFaker.Default().Generate();
        var moto = MotoEntityFaker.Default().Generate();
        var motoboy = MotoboyEntityFaker.DefaultWithTypeCnh(ECnhType.A).Generate();
        _motoRepository.Setup(x => x.GetByIdAsync(command.IdMoto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(moto);
        _motoboyRepository.Setup(x => x.GetByCnpjAsync(command.Cnpj, It.IsAny<CancellationToken>()))
            .ReturnsAsync(motoboy);
        _rentRepository.Setup(x => x.CreateAsync(It.IsAny<RentEntity>(), It.IsAny<CancellationToken>()));

        // Act
        var response = await _rentService.CreateRentAsync(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Success);
        _motoRepository.Verify(x => x.GetByIdAsync(command.IdMoto, It.IsAny<CancellationToken>()), Times.Once);
        _motoboyRepository.Verify(x => x.GetByCnpjAsync(command.Cnpj, It.IsAny<CancellationToken>()), Times.Once);
        _rentRepository.Verify(x => x.CreateAsync(It.IsAny<RentEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateRentAsync_WhenMotoNotFound_ShouldReturnError()
    {
        // Arrange
        var command = CreateRentCommandFaker.Default().Generate();
        _motoRepository.Setup(x => x.GetByIdAsync(command.IdMoto, It.IsAny<CancellationToken>()))
            .ReturnsAsync((MotoEntity?)null);


        // Act
        var response = await _rentService.CreateRentAsync(command, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.Errors.Should().Contain(x => x.Property == nameof(command.IdMoto));
        _motoRepository.Verify(x => x.GetByIdAsync(command.IdMoto, It.IsAny<CancellationToken>()), Times.Once);
        _rentRepository.Verify(x => x.CreateAsync(It.IsAny<RentEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CreateRentAsync_WhenMotoboyNotFound_ShouldReturnError()
    {
        // Arrange
        var command = CreateRentCommandFaker.Default().Generate();
        _motoboyRepository.Setup(x => x.GetByCnpjAsync(command.Cnpj, It.IsAny<CancellationToken>()))
            .ReturnsAsync((MotoboyEntity?)null);


        // Act
        var response = await _rentService.CreateRentAsync(command, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.Errors.Should().Contain(x => x.Property == nameof(command.IdMoto));
        _motoboyRepository.Verify(x => x.GetByCnpjAsync(command.Cnpj, It.IsAny<CancellationToken>()), Times.Once);
        _rentRepository.Verify(x => x.CreateAsync(It.IsAny<RentEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region CancelRentAsync

    [Fact]
    public async Task CancelRentAsync_WhenCommandIsValid_ShouldCancelRent()
    {
        // Arrange
        var command = CancelRentCommandFaker.Default().Generate();
        var rent = RentEntityFaker.Default().Generate();
        _rentRepository.Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(rent);
        _rentRepository.Setup(x => x.UpdateAsync(rent, It.IsAny<CancellationToken>()));
        var fine = rent.CalculateFine(DateOnly.FromDateTime(command.DatePreview));

        // Act
        var response = await _rentService.CancelRentAsync(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Success);
        response.Data.Should().Be("Valor da multa é de R$ " + fine);
        _rentRepository.Verify(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _rentRepository.Verify(x => x.UpdateAsync(rent, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CancelRentAsync_WhenRentNotFound_ShouldReturnError()
    {
        // Arrange
        var command = CancelRentCommandFaker.Default().Generate();
        _rentRepository.Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((RentEntity?)null);

        // Act
        var response = await _rentService.CancelRentAsync(command, CancellationToken.None);

        // Assert
        response.Success.Should().BeFalse();
        response.Errors.Should().Contain(x => x.Property == nameof(command.Id));
        _rentRepository.Verify(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _rentRepository.Verify(x => x.UpdateAsync(It.IsAny<RentEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }


    #endregion
}