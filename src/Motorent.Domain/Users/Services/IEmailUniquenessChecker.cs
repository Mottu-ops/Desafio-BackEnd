namespace Motorent.Domain.Users.Services;

public interface IEmailUniquenessChecker
{
    Task<bool> IsUniqueAsync(string email, CancellationToken cancellationToken = default);
}