﻿using FluentValidation;
using FluentValidation.Results;
using Job.Commons.Domain.Commands.User.Manager;
using Job.Commons.Domain.Entities.User;
using Job.Domain.Commands.User.Manager;
using Job.Domain.Entities.User;
using Job.Domain.Repositories;
using Job.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Job.UnitTests.Domain.Services;

public class ManagerServiceTest
{
    private readonly Mock<ILogger<ManagerService>> _logger = new();
    private readonly Mock<IManagerRepository> _managerRepository = new();
    private readonly Mock<IValidator<AuthenticationManagerCommand>> _validator = new();
    private readonly ManagerService _managerService;

    public ManagerServiceTest()
    {
        _managerService = new ManagerService(_logger.Object, _managerRepository.Object, _validator.Object);
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
        _managerRepository.Verify(x => x.GetAsync(command.Email, command.Password, It.IsAny<CancellationToken>()), Times.Once);
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
        Assert.Null(response);
        _managerRepository.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetManager_WhenCommandIsInvalid_ShouldReturnValidationErrors()
    {
        // Arrange
        var command = AuthenticationManagerCommandFaker.Invalid().Generate();
        var validationErrors = new List<ValidationFailure>
        {
            new("Email", "Email inválido")
        };
        _validator.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(validationErrors));

        // Act
        var response = await _managerService.GetManager(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response.Errors);
        _validator.Verify(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion
}