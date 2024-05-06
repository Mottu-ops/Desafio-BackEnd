using User.API.ViewModels;

namespace User.API.Interfaces.Auth
{
    public interface ITokenGenerator
    {
        JwtTokenResult GenerateToken(string email, EnumRole role);
        bool RemoveToken(string email);
    }
}
