using Motorent.Domain.Common.Enums;

namespace Motorent.Domain.Users.Enums;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role None = new("none", 0);
    public static readonly Role Admin = new("admin", 1);
    public static readonly Role Renter = new("renter", 2);

    public static readonly Role[] Valid = [Admin, Renter];

    private Role(string name, int value) : base(name, value)
    {
    }
}