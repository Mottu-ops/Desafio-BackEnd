using Motorent.Domain.Users.ValueObjects;

namespace Motorent.TestUtils.Constants;

public static partial class Constants
{
    public static class User
    {
        public static readonly UserId Id = UserId.New();
        
        public const string Name = "John Doe";
        
        public const string Email = "john@doe.com";
        
        public const string Password = "JohnDoe123";
        
        public static readonly DateOnly Birthdate = new(1990, 1, 1);
    }
}