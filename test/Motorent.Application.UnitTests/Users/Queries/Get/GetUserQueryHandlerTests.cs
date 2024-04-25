using Motorent.Application.Common.Abstractions.Identity;
using Motorent.Application.Common.Mappings;
using Motorent.Application.Users.Common.Mappings;
using Motorent.Application.Users.Queries.GetUser;
using Motorent.Domain.Users;
using Motorent.Domain.Users.Repository;

namespace Motorent.Application.UnitTests.Users.Queries.Get;

[TestSubject(typeof(GetUserQueryHandler))]
public sealed class GetUserQueryHandlerTests
{
    private readonly IUserContext userContext = A.Fake<IUserContext>();
    private readonly IUserRepository userRepository = A.Fake<IUserRepository>();
    private readonly CancellationToken cancellationToken = A.Dummy<CancellationToken>();

    private readonly GetUserQuery query = new();
    private readonly GetUserQueryHandler sut;

    public GetUserQueryHandlerTests()
    {
        sut = new GetUserQueryHandler(userContext, userRepository);

        A.CallTo(() => userContext.UserId)
            .Returns(Constants.User.Id);
        
        TypeAdapterConfig.GlobalSettings.Apply(new CommonMappings());
        TypeAdapterConfig.GlobalSettings.Apply(new UserMappings());
    }
    
    [Fact]
    public async Task Handle_WhenUserExists_ShouldReturnUserResponse()
    {
        // Arrange
        var user = (await Factories.User.CreateUserAsync()).Value;
        
        A.CallTo(() => userRepository.FindAsync(userContext.UserId, cancellationToken))
            .Returns(user);
        
        // Act
        var result = await sut.Handle(query, cancellationToken);
        
        // Assert
        result.Should().BeSuccess()
            .Which.Value.Should().BeEquivalentTo(new
            {
                Role = user.Role.Name,
                Name = user.Name.Value,
                Birthdate = user.Birthdate.Value,
                user.Email
            });
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExists_ShouldThrowApplicationException()
    {
        // Arrange
        A.CallTo(() => userRepository.FindAsync(userContext.UserId, cancellationToken))
            .Returns(null as User);
        
        // Act
        var act = () => sut.Handle(query, cancellationToken);
        
        // Assert
        await act.Should().ThrowExactlyAsync<ApplicationException>()
            .WithMessage($"User '{userContext.UserId}' not found");
    }
}