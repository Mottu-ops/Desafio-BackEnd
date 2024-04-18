namespace Motorent.Contracts.Users.Responses;

public sealed record UserResponse
{
    public string Role { get; init; } = null!;

    public string Name { get; init; } = null!;

    public string Email { get; init; } = null!;
    
    public DateOnly Birthdate { get; init; }
}

