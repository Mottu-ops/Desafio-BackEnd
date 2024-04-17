using Motorent.Domain.Common.Entities;
using Motorent.Domain.Users.Errors;
using Motorent.Domain.Users.Services;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.Domain.Users;

public sealed class User : Entity<UserId>, IAggregateRoot
{
    private User(UserId id) : base(id)
    {
    }

    public string Name { get; private set; } = null!;
    
    public string Email { get; private set; } = null!;

    public DateOnly Birthdate { get; private set; }

    public string PasswordHash { get; private set; } = null!;
    
    public static async Task<Result<User>> CreateAsync(
        UserId id,
        string name,
        string email,
        string password,
        DateOnly birthdate,
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
            Name = name,
            Email = email,
            Birthdate = birthdate,
            PasswordHash = passwordHash
        };
    }
}