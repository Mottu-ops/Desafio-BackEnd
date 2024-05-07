using Job.Commons.Domain.Commands.Moto;
using Job.Commons.Domain.Entities.Moto;
using Job.Commons.Domain.Entities.Rental;
using Job.Domain.Entities.Moto;

namespace Job.UnitTests.Domain.Services;

[Trait("Services", "MotoService")]
public sealed class MotoServiceTest
{
    private readonly Mock<IMotoRepository> _motoRepository = new();
    private readonly Mock<ILogger<MotoService>> _logger = new();
    private readonly Mock<IRentalRepository> _rentRepository = new();
    private readonly MotoService _motoService;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    public MotoServiceTest()
    {
        _motoService = new MotoService(_logger.Object, _motoRepository.Object, _rentRepository.Object);
    }

    #region CreateAsync

    [Fact]
    public async Task CreateAsync_WhenCommandIsValid_ShouldCreateMoto()
    {
        // Arrange
        var command = CreateMotoCommandFaker.Default().Generate();
        _motoRepository.Setup(x => x.CheckPlateExistsAsync(command.Plate, _cancellationToken))
            .ReturnsAsync(false);

        // Act
        var response = await _motoService.CreateAsync(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Success);
        _motoRepository.Verify(x => x.CreateAsync(It.IsAny<MotoEntity>(), _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_WhenPlateExists_ShouldReturnError()
    {
        // Arrange
        var command = CreateMotoCommandFaker.Default().Generate();
        _motoRepository.Setup(x => x.CheckPlateExistsAsync(command.Plate, _cancellationToken))
            .ReturnsAsync(true);

        // Act
        var response = await _motoService.CreateAsync(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.Success);
        _motoRepository.Verify(x => x.CreateAsync(It.IsAny<MotoEntity>(), _cancellationToken), Times.Never);
    }

    #endregion

    #region UpdateAsync

    [Fact]
    public async Task UpdateAsync_WhenCommandIsValid_ShouldUpdateMoto()
    {
        // Arrange
        var entity = MotoEntityFaker.Default().Generate();
        var command = UpdateMotoCommandFaker.Default().Generate();
        _motoRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), _cancellationToken))
            .ReturnsAsync(entity);

        // Act
        var response = await _motoService.UpdateAsync(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Success);
        _motoRepository.Verify(x => x.UpdateAsync(It.IsAny<MotoEntity>(), _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenMotoNotFound_ShouldReturnError()
    {
        // Arrange
        var command = UpdateMotoCommandFaker.Default().Generate();
        _motoRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), _cancellationToken))
            .ReturnsAsync((MotoEntity?)null);

        // Act
        var response = await _motoService.UpdateAsync(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.Success);
        _motoRepository.Verify(x => x.UpdateAsync(It.IsAny<MotoEntity>(), _cancellationToken), Times.Never);
    }

    #endregion

    #region DeleteAsync

    [Fact]
    public async Task DeleteAsync_WhenMotoExists_ShouldDeleteMoto()
    {
        // Arrange
        var entity = MotoEntityFaker.Default().Generate();
        _motoRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), _cancellationToken))
            .ReturnsAsync(entity);

        // Act
        var response = await _motoService.DeleteAsync(entity.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Success);
        _motoRepository.Verify(x => x.DeleteAsync(It.IsAny<MotoEntity>(), _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenMotoNotFound_ShouldReturnError()
    {
        // Arrange
        _motoRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), _cancellationToken))
            .ReturnsAsync((MotoEntity?)null);

        // Act
        var response = await _motoService.DeleteAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.Success);
        _motoRepository.Verify(x => x.DeleteAsync(It.IsAny<MotoEntity>(), _cancellationToken), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_WhenMotoHasRent_ShouldReturnError()
    {
        // Arrange
        var motoEntity = MotoEntityFaker.Default().Generate();
        var rentEntity = RentalEntityFaker.Default().Generate();
        _motoRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), _cancellationToken))
            .ReturnsAsync(motoEntity);
        _rentRepository.Setup(x => x.GetByMotoIdAsync(It.IsAny<Guid>(), _cancellationToken))
            .ReturnsAsync(rentEntity);

        // Act
        var response = await _motoService.DeleteAsync(motoEntity.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.False(response.Success);
        _motoRepository.Verify(x => x.DeleteAsync(It.IsAny<MotoEntity>(), _cancellationToken), Times.Never);
    }

    #endregion

    #region GetAllAsync

    [Fact]
    public async Task GetAllAsync_WhenMotosExists_ShouldReturnMotos()
    {
        // Arrange
        var entities = MotoEntityFaker.Default().Generate(30);
        _motoRepository.Setup(x => x.GetAllAsync(_cancellationToken))
            .ReturnsAsync(entities);

        // Act
        var response = await _motoService.GetAllAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response);
    }

    [Fact]
    public async Task GetAllAsync_WhenMotosNotExists_ShouldReturnEmptyList()
    {
        // Arrange
        _motoRepository.Setup(x => x.GetAllAsync(_cancellationToken))
            .ReturnsAsync(new List<MotoEntity>());

        // Act
        var response = await _motoService.GetAllAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Empty(response);
    }

    #endregion

    #region GetByIdAsync

    [Fact]
    public async Task GetByIdAsync_WhenMotoExists_ShouldReturnMoto()
    {
        // Arrange
        var entity = MotoEntityFaker.Default().Generate();
        _motoRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), _cancellationToken))
            .ReturnsAsync(entity);

        // Act
        var response = await _motoService.GetByIdAsync(entity.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task GetByIdAsync_WhenMotoNotExists_ShouldReturnNull()
    {
        // Arrange
        _motoRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), _cancellationToken))
            .ReturnsAsync((MotoEntity?)null);

        // Act
        var response = await _motoService.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);

        // Assert
        Assert.Null(response);
    }

    #endregion

    #region GetByPlaceAsync

    [Fact]
    public async Task GetByPlateAsync_WhenMotoExists_ShouldReturnMoto()
    {
        // Arrange
        var entity = MotoEntityFaker.Default().Generate();
        _motoRepository.Setup(x => x.GetByPlateAsync(It.IsAny<string>(), _cancellationToken))
            .ReturnsAsync(entity);

        // Act
        var response = await _motoService.GetByPlateAsync(entity.Plate, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task GetByPlateAsync_WhenMotoNotExists_ShouldReturnNull()
    {
        // Arrange
        _motoRepository.Setup(x => x.GetByPlateAsync(It.IsAny<string>(), _cancellationToken))
            .ReturnsAsync((MotoEntity?)null);

        // Act
        var response = await _motoService.GetByPlateAsync(It.IsAny<string>(), CancellationToken.None);

        // Assert
        Assert.Null(response);
    }
    #endregion
}