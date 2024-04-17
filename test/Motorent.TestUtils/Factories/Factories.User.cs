using Motorent.Domain.Users.Services;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.TestUtils.Factories;

public static partial class Factories
{
    public static class User
    {
        public static Task<Result<Domain.Users.User>> CreateUserAsync(
            UserId? id = null,
            string? name = null,
            string? email = null,
            string? password = null,
            DateOnly? birthdate = null,
            IEncryptionService? encryptionService = null,
            IEmailUniquenessChecker? emailUniquenessChecker = null)
        {
            if (encryptionService is null)
            {
                encryptionService = A.Fake<IEncryptionService>();
                A.CallTo(() => encryptionService.Encrypt(A<string>._))
                    .Returns($"Hashed {Constants.Constants.User.Password}");
            }

            if (emailUniquenessChecker is null)
            {
                emailUniquenessChecker = A.Fake<IEmailUniquenessChecker>();
                A.CallTo(() => emailUniquenessChecker.IsUniqueAsync(A<string>._, A<CancellationToken>._))
                    .Returns(true);
            }

            return Domain.Users.User.CreateAsync(
                id: id ?? Constants.Constants.User.Id,
                name: name ?? Constants.Constants.User.Name,
                email: email ?? Constants.Constants.User.Email,
                password: password ?? Constants.Constants.User.Password,
                birthdate: birthdate ?? Constants.Constants.User.Birthdate,
                encryptionService: encryptionService,
                emailUniquenessChecker: emailUniquenessChecker);
        }
        
    }
}