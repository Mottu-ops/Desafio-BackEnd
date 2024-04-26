using Motorent.Domain.Common.ValueObjects;
using Motorent.Domain.Users.Enums;
using Motorent.Domain.Users.ValueObjects;
using Motorent.TestUtils.Constants;

namespace Motorent.Api.IntegrationTests.Common;

public sealed record UserData(
    UserId UserId,
    Role Role,
    Name Name,
    Birthdate Birthdate,
    string Email,
    string Password)
{
    public static readonly UserData Default = new(
        Constants.User.Id,
        Constants.User.Role,
        Constants.User.Name,
        Constants.User.Birthdate,
        Constants.User.Email,
        Constants.User.Password);
}