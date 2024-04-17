using Motorent.Domain.Users.Enums;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.Application.Common.Abstractions.Identity;

public interface IUserContext
{
    bool IsAuthenticated { get; }
    
    UserId UserId { get; }
    
    Role Role { get; }
}