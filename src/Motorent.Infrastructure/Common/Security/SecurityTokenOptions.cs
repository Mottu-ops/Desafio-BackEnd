using System.ComponentModel.DataAnnotations;

namespace Motorent.Infrastructure.Common.Security;

internal sealed record SecurityTokenOptions
{
    public const string SectionName = "SecureToken";

    [Required]
    public string Key { get; init; } = null!;

    [Required]
    public string Issuer { get; init; } = null!;

    [Required]
    public string Audience { get; init; } = null!;

    [Required, Range(1, int.MaxValue)]
    public int ExpiresInMinutes { get; init; }

    [Required, Range(1, int.MaxValue)]
    public int RefreshTokenExpiresInMinutes { get; init; }
}