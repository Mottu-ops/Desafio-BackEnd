using Motorent.Domain.Common.Enums;

namespace Motorent.Domain.Users.Enums;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Admin = new Role("admin", 1);
    public static readonly Role Renter = new Role("renter", 2);
    
    private Role(string name, int value) : base(name, value)
    {
    }
}