using Motorent.Contracts.Users.Requests;

namespace Motorent.Api.IntegrationTests.TestUtils.Requests;

internal static partial class Requests
{
    public static class User
    {
        public static readonly LoginRequest LoginRequest = new()
        {
            Email = "john@doe.com",
            Password = "JohnDoe123"
        };

        public static readonly RegisterRequest RegisterRequest = new()
        {
            Role = "admin",
            Name = "John Doe",
            Email = "john@doe.com",
            Password = "JohnDoe123",
            Birthdate = new DateOnly(2000, 9, 5)
        };

        public static HttpRequestMessage LoginHttpRequest(LoginRequest request) =>
            Post("v1/users/login", request);

        public static HttpRequestMessage RegisterHttpRequest(RegisterRequest request) =>
            Post("v1/users/register", request);

        public static HttpRequestMessage RefreshTokenHttpRequest(RefreshTokenRequest request) =>
            Post("v1/users/refresh-token", request);
        
        public static HttpRequestMessage GetUserHttpRequest() => Get("v1/users");
    }
}