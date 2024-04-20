using System.Text.Json;

namespace Motorent.Api.IntegrationTests.TestUtils;

internal static class ApiSerializationOptions
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };
}