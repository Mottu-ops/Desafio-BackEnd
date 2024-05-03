using Job.Commons.Domain.Commands.User.Manager;
using Job.Commons.Domain.Entities.User;
using Job.Domain.Commands.User.Manager;
using Job.Domain.Commands.User.Manager.Validations;
using Job.Domain.Commons;
using Job.Domain.Entities.User;

namespace Job.UnitTests.Domain.Services;

[Trait("Services", "ManagerService")]
public class ManagerServiceTest
{
    private readonly Mock<ILogger<ManagerService>> _logger = new();
    private readonly Mock<IManagerRepository> _managerRepository = new();
    private readonly IValidator<AuthenticationManagerCommand> _validator = new AuthenticationManagerValidation();
    private readonly ManagerService _managerService;

    public ManagerServiceTest()
    {
        _managerService = new ManagerService(_logger.Object, _managerRepository.Object, _validator);
    }

    #region GetManager

    [Fact]
    public async Task GetManager_WhenCommandIsValid_ShouldReturnManager()
    {
        // Arrange
        var command = AuthenticationManagerCommandFaker.Default().Generate();
        var manager = ManagerEntityFaker.Default().Generate();
        _managerRepository.Setup(x => x.GetAsync(command.Email, command.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(manager);

        // Act
        var response = await _managerService.GetManager(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        _managerRepository.Verify(x => x.GetAsync(command.Email, Cryptography.Encrypt(command.Password), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetManager_WhenCommandIsInvalid_ShouldReturnNull()
    {
        // Arrange
        var command = AuthenticationManagerCommandFaker.Invalid().Generate();
        _managerRepository.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ManagerEntity?)null);

        // Act
        var response = await _managerService.GetManager(command, CancellationToken.None);

        // Assert
        response.Errors.Should().HaveCount(2);
        _managerRepository.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetManager_WhenCommandIsNull_ShouldReturnNull()
    {
        // Arrange
        var command = AuthenticationManagerCommandFaker.Default().Generate();
        _managerRepository.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ManagerEntity?)null);

        // Act
        var response = await _managerService.GetManager(command, CancellationToken.None);

        // Assert
        response.Data.Should().BeNull();
        response.Errors.Should().HaveCount(0);
        _managerRepository.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion
}