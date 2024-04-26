using System.Text;
using System.Text.Json;

namespace Motorent.Api.IntegrationTests.TestUtils.Requests;

internal static partial class Requests
{
    private const string BaseUrl = "/api";
    
    private const string MediaType = "application/json";
    
    private static readonly Encoding Encoding = Encoding.UTF8;
    
    private static string Serialize<T>(T value) => JsonSerializer.Serialize(value, ApiSerializationOptions.Options);
    
    private static HttpRequestMessage Post(string path, object request) =>
        new(HttpMethod.Post, $"{BaseUrl}/{path}")
        {
            Content = new StringContent(Serialize(request), Encoding, MediaType)
        };
    
    private static HttpRequestMessage Get(string path) => new(HttpMethod.Get, $"{BaseUrl}/{path}");
}