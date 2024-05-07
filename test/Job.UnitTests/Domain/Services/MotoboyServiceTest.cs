using Job.Commons.Domain.Commands.User.Motoboy;
using Job.Commons.Domain.Entities.User;
using Job.Domain.Commons;
using Job.Domain.Entities.User;

namespace Job.UnitTests.Domain.Services;

[Trait("Services", "MotoboyService")]
public class MotoboyServiceTest
{
    private readonly Mock<ILogger<MotoboyService>> _logger = new();
    private readonly Mock<IMotoboyRepository> _managerRepository = new();
    private readonly MotoboyService _motoboyService;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    public MotoboyServiceTest()
    {
        _motoboyService = new MotoboyService(_logger.Object, _managerRepository.Object);
    }

    #region GetMotoboy

    [Fact]
    public async Task GetMotoboy_WhenCommandIsValid_ShouldReturnMotoboy()
    {
        // Arrange
        var command = AuthenticationMotoboyCommandFaker.Default().Generate();
        var motoboy = MotoboyEntityFaker.Default().Generate();
        _managerRepository.Setup(x => x.GetAsync(It.IsAny<string>(), command.Password, _cancellationToken))
            .ReturnsAsync(motoboy);

        // Act
        var response = await _motoboyService.GetMotoboy(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        _managerRepository.Verify(x => x.GetAsync(It.IsAny<string>(), Cryptography.Encrypt(command.Password), _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetMotoboy_WhenMotoboyIsEmpty_ShouldReturnMotoboy()
    {
        // Arrange
        var command = AuthenticationMotoboyCommandFaker.Default().Generate();
        _managerRepository.Setup(x => x.GetAsync(It.IsAny<string>(), command.Password, _cancellationToken))
            .ReturnsAsync((MotoboyEntity?)null);

        // Act
        var response = await _motoboyService.GetMotoboy(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        _managerRepository.Verify(x => x.GetAsync(It.IsAny<string>(), Cryptography.Encrypt(command.Password), _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task GetMotoboy_WhenCommandIsInvalid_ShouldReturnNull()
    {
        // Arrange
        var command = AuthenticationMotoboyCommandFaker.Invalid().Generate();
        _managerRepository.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), _cancellationToken))
            .ReturnsAsync((MotoboyEntity?)null);

        // Act
        var response = await _motoboyService.GetMotoboy(command, CancellationToken.None);

        // Assert
        response.Errors.Should().HaveCount(2);
        _managerRepository.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), _cancellationToken), Times.Never);
    }

    #endregion

    #region CreateAsync

    [Fact]
    public async Task CreateAsync_WhenCommandIsValid_ShouldCreateMotoboy()
    {
        // Arrange
        var command = CreateMotoboyCommandFaker.Default().Generate();

        // Act
        var response = await _motoboyService.CreateAsync(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        _managerRepository.Verify(x => x.CreateAsync(It.IsAny<MotoboyEntity>(), _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_WhenCnpjIsExist_ShouldCreateMotoboy()
    {
        // Arrange
        var command = CreateMotoboyCommandFaker.Default().Generate();
        _managerRepository.Setup(x => x.CheckCnpjExistsAsync(It.IsAny<string>(), _cancellationToken))
            .ReturnsAsync(true);

        // Act
        var response = await _motoboyService.CreateAsync(command, CancellationToken.None);

        // Assert
        response.Errors.Should().HaveCount(1);
        _managerRepository.Verify(x => x.CreateAsync(It.IsAny<MotoboyEntity>(), _cancellationToken), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_WhenCnhIsExist_ShouldCreateMotoboy()
    {
        // Arrange
        var command = CreateMotoboyCommandFaker.Default().Generate();
        _managerRepository.Setup(x => x.CheckCnhExistsAsync(It.IsAny<string>(), _cancellationToken))
            .ReturnsAsync(true);

        // Act
        var response = await _motoboyService.CreateAsync(command, CancellationToken.None);

        // Assert
        response.Errors.Should().HaveCount(1);
        _managerRepository.Verify(x => x.CreateAsync(It.IsAny<MotoboyEntity>(), _cancellationToken), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_WhenCommandIsInvalid_ShouldReturnErrors()
    {
        // Arrange
        var command = CreateMotoboyCommandFaker.Invalid().Generate();

        // Act
        var response = await _motoboyService.CreateAsync(command, CancellationToken.None);

        // Assert
        response.Errors.Should().HaveCount(5);
        _managerRepository.Verify(x => x.CreateAsync(It.IsAny<MotoboyEntity>(), _cancellationToken), Times.Never);
    }

    #endregion


}