using Motorent.Domain.Renters.Errors;
using Motorent.Domain.Renters.Services;
using Motorent.Domain.Renters.ValueObjects;

namespace Motorent.Domain.UnitTests.Renters.Services;

[TestSubject(typeof(RenterFactory))]
public sealed class RenterFactoryTests
{
    private readonly ICnpjUniquenessChecker cnpjUniquenessChecker = A.Fake<ICnpjUniquenessChecker>();
    private readonly ICnhUniquenessChecker cnhUniquenessChecker = A.Fake<ICnhUniquenessChecker>();
    private readonly CancellationToken cancellationToken = A.Dummy<CancellationToken>();
    
    private readonly RenterFactory sut;

    public RenterFactoryTests()
    {
        sut = new RenterFactory(cnpjUniquenessChecker, cnhUniquenessChecker);
        
        A.CallTo(() => cnpjUniquenessChecker.IsUniqueAsync(A<Cnpj>._, cancellationToken))
            .Returns(true);
        
        A.CallTo(() => cnhUniquenessChecker.IsUniqueAsync(A<Cnh>._, cancellationToken))
            .Returns(true);
    }

    [Fact]
    public async Task CreateAsync_WhenCnpjIsNotUnique_ShouldReturnDuplicateCnpj()
    {
        // Arrange
        var user = await Factories.User.CreateUserAsync();
        var renterId = Constants.Renter.Id;
        var cnpj = Constants.Renter.Cnpj;
        var cnh = Constants.Renter.Cnh;
        
        A.CallTo(() => cnpjUniquenessChecker.IsUniqueAsync(cnpj, cancellationToken))
            .Returns(false);
        
        // Act
        var result = await sut.CreateAsync(user.Value, renterId, cnpj, cnh, cancellationToken);
        
        // Assert
        result.Should().BeFailure(RenterErrors.DuplicateCnpj);
    }
    
    [Fact]
    public async Task CreateAsync_WhenCnhIsNotUnique_ShouldReturnDuplicateCnh()
    {
        // Arrange
        var user = await Factories.User.CreateUserAsync();
        var renterId = Constants.Renter.Id;
        var cnpj = Constants.Renter.Cnpj;
        var cnh = Constants.Renter.Cnh;
        
        A.CallTo(() => cnhUniquenessChecker.IsUniqueAsync(cnh, cancellationToken))
            .Returns(false);
        
        // Act
        var result = await sut.CreateAsync(user.Value, renterId, cnpj, cnh, cancellationToken);
        
        // Assert
        result.Should().BeFailure(RenterErrors.DuplicateCnh);
    }
    
    [Fact]
    public async Task CreateAsync_WhenCnpjAndCnhAreUnique_ShouldReturnRenter()
    {
        // Arrange
        var user = await Factories.User.CreateUserAsync();
        var renterId = Constants.Renter.Id;
        var cnpj = Constants.Renter.Cnpj;
        var cnh = Constants.Renter.Cnh;
        
        // Act
        var result = await sut.CreateAsync(user.Value, renterId, cnpj, cnh, cancellationToken);
        
        // Assert
        result.Should().BeSuccess()
            .Which.Value.Should().BeEquivalentTo(new
            {
                Id = renterId,
                UserId = user.Value.Id,
                Cnpj = cnpj,
                Cnh = cnh,
                user.Value.Name,
                user.Value.Birthdate
            });
    }
}