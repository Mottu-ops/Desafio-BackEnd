using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.API.Interfaces.Auth;
using User.API.Models;
using User.API.Utils;
using User.API.ViewModels;
using User.Services.Interfaces;

namespace User.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IClientServices _clientServices;
        private readonly IJwtService _jwtService;

        public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator, IClientServices clientServices, IJwtService jwtService)
        {
            _clientServices = clientServices;
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("/auth/login")]
        public async Task<ActionResult> SignIn([FromBody] AuthModel authModel)
        {
            try
            {
                var user = await _clientServices.GetByEmail(authModel.Login);
                if (user.Password == authModel.Password)
                {
                    var tokenResult = _tokenGenerator.GenerateToken(user.Email, (EnumRole)user.Role);

                    return Ok(new BaseResultModel
                    {
                        Message = "User Authorized",
                        Success = true,
                        MetaData = new
                        {
                            Token = tokenResult.Token,
                            Expiration = tokenResult.Expiration
                        }
                    });

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, GenericResponse.GenericApplicationError(ex.Data));
            }
            return Unauthorized();
        }

        [HttpGet]
        [Authorize]
        [Route("/auth/logoff")]
        public async Task<ActionResult> LogOff()
        {
            try
            {
                var loggedInUser = _jwtService.GetLoggedInUser();
                if (loggedInUser != null)
                {
                    bool logoff = _tokenGenerator.RemoveToken(loggedInUser.Email);

                    return Ok(new BaseResultModel
                    {
                        Message = "LogOff Completed",
                        Success = logoff,
                        MetaData = { }
                    });

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, GenericResponse.GenericApplicationError(ex.Data));

            }
            return Unauthorized();
        }

    }
}
