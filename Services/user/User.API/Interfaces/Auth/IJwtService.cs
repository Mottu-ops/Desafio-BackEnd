using User.Services.Models;

namespace User.API.Interfaces.Auth
{
    public interface IJwtService
    {
        public LoggedUser GetLoggedInUser();
    }
}
