using System.Security.Claims;
using User.API.Interfaces.Auth;
using User.API.ViewModels;
using User.Services.Interfaces;
using User.Services.Models;

namespace User.Services.Service
{
    public class JwtService : IJwtService
    {
        private readonly IHttpContextAccessor _httpContextAccessor; 


        public JwtService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public LoggedUser GetLoggedInUser()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var roleString = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            EnumRole roleEnum;
            Enum.TryParse(roleString, out roleEnum);

            return new LoggedUser
            {
                Email = email!,
                Role = roleEnum
            };
        }

    }
}
