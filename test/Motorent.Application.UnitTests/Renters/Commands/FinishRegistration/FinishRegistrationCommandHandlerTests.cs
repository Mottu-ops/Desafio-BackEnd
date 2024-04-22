using Motorent.Application.Common.Abstractions.Identity;
using Motorent.Application.Common.Mappings;
using Motorent.Application.Renters.Commands.FinishRegistration;
using Motorent.Application.Renters.Common.Mappings;
using Motorent.Contracts.Renters.Responses;
using Motorent.Domain.Renters;
using Motorent.Domain.Renters.Repository;
using Motorent.Domain.Renters.Services;
using Motorent.Domain.Renters.ValueObjects;
using Motorent.Domain.Users;
using Motorent.Domain.Users.Repository;

namespace Motorent.Application.UnitTests.Renters.Commands.FinishRegistration;

[TestSubject(typeof(FinishRegistrationCommandHandler))]
public sealed class FinishRegistrationCommandHandlerTests
{
    private readonly IUserContext userContext = A.Fake<IUserContext>();
    private readonly IUserRepository userRepository = A.Fake<IUserRepository>();
    private readonly IRenterRepository renterRepository = A.Fake<IRenterRepository>();
    private readonly IRenterFactory renterFactory = A.Fake<IRenterFactory>();
    private readonly CancellationToken cancellationToken = A.Dummy<CancellationToken>();

    private readonly FinishRegistrationCommand command = new()
    {
        Cnpj = Constants.Renter.Cnpj.Value,
        CnhNumber = Constants.Renter.Cnh.Number.Value,
        CnhCategory = Constants.Renter.Cnh.Category.Name,
        CnhExpirationDate = Constants.Renter.Cnh.ExpirationDate
    };

    private readonly FinishRegistrationCommandHandler sut;

    private readonly User user = Factories.User.CreateUserAsync().Result.Value;

    public FinishRegistrationCommandHandlerTests()
    {
        TypeAdapterConfig.GlobalSettings.Apply(new CommonMappings());
        TypeAdapterConfig.GlobalSettings.Apply(new RenterMappings());

        sut = new FinishRegistrationCommandHandler(
            userContext,
            userRepository,
            renterRepository,
            renterFactory);

        A.CallTo(() => userRepository.FindAsync(userContext.UserId, cancellationToken))
            .Returns(user);
    }

    [Fact]
    public async Task Handle_WhenCommandIsValid_ShouldCreateRenterAndSaveToRepository()
    {
        // Arrange
        var renter = Factories.Renter.CreateRenter();

        A.CallTo(() => renterFactory.CreateAsync(
                user,
                A<RenterId>._,
                Constants.Renter.Cnpj,
                Constants.Renter.Cnh,
                cancellationToken))
            .Returns(renter);

        // Act
        await sut.Handle(command, cancellationToken);

        // Assert
        A.CallTo(() => renterRepository.AddAsync(
                A<Renter>.That.Matches(r => r.UserId == user.Id
                                            && r.Cnpj == Constants.Renter.Cnpj
                                            && r.Cnh == Constants.Renter.Cnh),
                cancellationToken))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_WhenCommandIsValid_ShouldReturnTokenResponse()
    {
        // Arrange
        var renter = Factories.Renter.CreateRenter();

        A.CallTo(() => renterFactory.CreateAsync(
                user,
                A<RenterId>._,
                Constants.Renter.Cnpj,
                Constants.Renter.Cnh,
                cancellationToken))
            .Returns(renter);

        // Act
        var result = await sut.Handle(command, cancellationToken);

        // Assert
        result.Should().BeSuccess(renter.Adapt<RenterResponse>());
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ShouldThrowApplicationException()
    {
        // Arrange
        A.CallTo(() => userRepository.FindAsync(userContext.UserId, cancellationToken))
            .Returns(null as User);

        // Act
        var act = () => sut.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowExactlyAsync<ApplicationException>()
            .WithMessage($"User {userContext.UserId} not found");
    }

    [Fact]
    public async Task Handle_WhenCnpjIsInvalid_ShouldReturnErrors()
    {
        // Arrange
        const string cnpj = "27137093000134";

        // Act
        var result = await sut.Handle(command with { Cnpj = cnpj }, cancellationToken);

        // Assert
        result.Should().BeFailure();
    }

    [Fact]
    public async Task Handle_WhenCnhNumberIsInvalid_ShouldReturnErrors()
    {
        // Arrange
        const string cnhNumber = "12345678901";

        // Act
        var result = await sut.Handle(command with { CnhNumber = cnhNumber }, cancellationToken);

        // Assert
        result.Should().BeFailure();
    }

    [Fact]
    public async Task Handle_WhenCnhIsInvalid_ShouldReturnErrors()
    {
        // Arrange
        var expirationDate = new DateOnly(2020, 1, 1);

        // Act
        var result = await sut.Handle(command with { CnhExpirationDate = expirationDate }, cancellationToken);

        // Assert
        result.Should().BeFailure();
    }
}