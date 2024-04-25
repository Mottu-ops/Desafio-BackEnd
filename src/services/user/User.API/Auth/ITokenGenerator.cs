namespace User.API.Auth;

public interface ITokenGenerator {
    string GenerateToken(string login, string role);
}