namespace Motorent.Application.Common.Abstractions.Security.Authorization;

internal sealed record AuthorizationResult
{
    private AuthorizationResult()
    {
    }

    public bool Succeeded { get; private init; } = true;

    public string? Reason { get; private init; }

    public override string ToString() => Succeeded ? "Succeeded" : $"Failed: {Reason}";

    public static AuthorizationResult Success() => new();

    public static AuthorizationResult Failure(string reason) => new()
    {
        Reason = reason,
        Succeeded = false
    };
}