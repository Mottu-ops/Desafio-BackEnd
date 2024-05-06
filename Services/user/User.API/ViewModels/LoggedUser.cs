using User.API.ViewModels;

namespace User.Services.Models
{

    public class LoggedUser
    {
        public string Email { get; set; }
        public EnumRole Role { get; set; }
    }

}
