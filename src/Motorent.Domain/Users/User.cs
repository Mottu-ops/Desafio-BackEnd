using Motorent.Domain.Common.Entities;
using Motorent.Domain.Common.ValueObjects;
using Motorent.Domain.Users.Enums;
using Motorent.Domain.Users.Errors;
using Motorent.Domain.Users.Services;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.Domain.Users;

public sealed class User : Entity<UserId>, IAggregateRoot
{
    private User(UserId id) : base(id)
    {
    }

    // Essa é uma maneira bem simples de tratar usuários com diferentes papéis. A idéia é que a entidade User seja como
    // uma entidade generalizada, representando todos os usuários (admin e locatário). Os usuários cujo papel seja
    // locatário terão a entidade Renter relacionada a esta entidade. O papel do usuário é definido no momento da
    // criação do usuário. Talvez, no futuro, eu delegue a responsabilidade de gerenciar usuários para algum serviço
    // externo, como Amazon Cognito, Auth0, Firebase Auth ou outro.
    public required Role Role { get; init; }
    
    public Name Name { get; private set; } = null!;

    public Birthdate Birthdate { get; private set; } = null!;
    
    public string Email { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;
    
    public static async Task<Result<User>> CreateAsync(
        Role role,
        UserId id,
        Name name,
        Birthdate birthdate,
        string email,
        string password,
        IEncryptionService encryptionService,
        IEmailUniquenessChecker emailUniquenessChecker,
        CancellationToken cancellationToken = default)
    {
        if (!await emailUniquenessChecker.IsUniqueAsync(email, cancellationToken))
        {
            return UserErrors.DuplicateEmail(email);
        }

        var passwordHash = encryptionService.Encrypt(password);
        return new User(id)
        {
            Role = role,
            Name = name,
            Birthdate = birthdate,
            Email = email,
            PasswordHash = passwordHash
        };
    }
}