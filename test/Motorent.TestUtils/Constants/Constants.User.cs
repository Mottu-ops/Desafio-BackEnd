using Motorent.Domain.Common.ValueObjects;
using Motorent.Domain.Users.Enums;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.TestUtils.Constants;

public static partial class Constants
{
    public static class User
    {
        public static readonly UserId Id = UserId.New();
        
        public static readonly Role Role = Role.Renter;
        
        public static readonly Name Name = new("John Doe");
        
        public static readonly Birthdate Birthdate = Birthdate.Create(new(1990, 1, 1)).Value;
        
        public const string Email = "john@doe.com";
        
        public const string Password = "JohnDoe123";
    }
}