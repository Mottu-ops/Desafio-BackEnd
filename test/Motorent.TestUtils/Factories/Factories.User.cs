using Motorent.Domain.Common.ValueObjects;
using Motorent.Domain.Users.Enums;
using Motorent.Domain.Users.Services;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.TestUtils.Factories;

public static partial class Factories
{
    public static class User
    {
        public static Task<Result<Domain.Users.User>> CreateUserAsync(
            UserId? id = null,
            Role? role = null,
            Name? name = null,
            Birthdate? birthdate = null,
            string? email = null,
            string? password = null,
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
                role: role ?? Constants.Constants.User.Role,
                name: name ?? Constants.Constants.User.Name,
                birthdate: birthdate ?? Constants.Constants.User.Birthdate,
                email: email ?? Constants.Constants.User.Email,
                password: password ?? Constants.Constants.User.Password,
                encryptionService: encryptionService,
                emailUniquenessChecker: emailUniquenessChecker);
        }
        
    }
}