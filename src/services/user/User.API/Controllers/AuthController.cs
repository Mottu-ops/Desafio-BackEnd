using Microsoft.AspNetCore.Mvc;
using User.API.Auth;
using User.API.Utilities;
using User.API.ViewModels;
using User.Service.Interfaces;

namespace User.API.Controllers;
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IPartnerServices _partnerServices;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IConfiguration _configuration;


    public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator, IPartnerServices partnerServices)
    {
        _configuration = configuration;
        _tokenGenerator = tokenGenerator;
        _partnerServices = partnerServices;
    }
    [HttpPost]
    [Route("api/v1/auth/login")]
    public async Task<IActionResult> SignIn([FromBody] AuthViewModel authViewModel)
    {
        try
        {
            var user = await _partnerServices.Get(authViewModel.Login);
            if(user.Password == authViewModel.Password) {
                return Ok(new ResultViewModel{
                    Message = "User authenticated successfully.",
                    Success = true,
                    Data = new {
                        Token = _tokenGenerator.GenerateToken(user.Email, "User"),
                        TokenExpiration = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"])),
                    }
                });
            }
            else{
                return Unauthorized();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage(ex.Data));
        }
    }
}