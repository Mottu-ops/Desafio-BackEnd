using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using MotorcycleRental.Api.Core.Identity;
using MotorcycleRental.Authentication.Api.Configuration;
using MotorcycleRental.Authentication.Api.Models.Register;
using MotorcycleRental.Authentication.Api.Models.Token;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Infraestructure.Repositories;
using RabbitMqMessage.Service;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace MotorcycleRental.Authentication.Api.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    //[Authorize]
    public class AuthController : BaseController
    {
        private readonly IRabbitMqPublish _rabbitMqPublish;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly RegisterQueueSettings _registerQueueSettings;
        private readonly IDeliverymanRepository _deliverymanRepository;

        private string _email;


        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<RegisterQueueSettings> registerQueueSettings,
                              IOptions<AppSettings> appSettings,
                              IRabbitMqPublish rabbitMqPublish,
                              IDeliverymanRepository deliverymanRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _rabbitMqPublish = rabbitMqPublish;
            _registerQueueSettings = registerQueueSettings.Value;
            _deliverymanRepository = deliverymanRepository;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserLoginViewModel userLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (result.Succeeded)
                return CustomResponse(await GerarToken(userLogin.Email));

            _email = userLogin.Email;

            if (result.IsLockedOut)
            {
                AddError("Usuário temporáriamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AddError("Usuário ou senha incorretos");
            return CustomResponse();
        }

        //[ClaimsAuthorize("Authentication", "Master")]
        [AllowAnonymous]
        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult> RegisterAdmin(UserAdminRegisterViewModel userRegister)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                await AddClaims(userRegister.Email, new UserClaimViewModel() { Type = "Admin", Value = "admin" });
                await AddClaims(userRegister.Email, new UserClaimViewModel() { Type = "Deliveryman", Value = "user" });

                var token = CustomResponse(await GerarToken(userRegister.Email));

                return token;
            }


            foreach (var error in result.Errors)
                AddError(error.Description);

            return CustomResponse();
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserRegisterViewModel userRegister)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true
            };

            if (!await IsValidMandatoryInformation(userRegister))
                return CustomResponse();

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                await AddClaims(userRegister.Email, new UserClaimViewModel() { Type = "Deliveryman", Value = "user" });

                var token = CustomResponse(await GerarToken(userRegister.Email));

                var _user = await _userManager.FindByEmailAsync(userRegister.Email);

                var newUser = new NewUserRegisterViewModel(Guid.Parse(_user.Id),
                                                           userRegister.Email,
                                                           userRegister.Name,
                                                           userRegister.Cnpj,
                                                           userRegister.DateOfBirth,
                                                           userRegister.DriverLicenseNumber,
                                                           userRegister.DriverLicenseType);

                SendMessage(newUser);
                return token;
            }


            foreach (var error in result.Errors)
                AddError(error.Description);

            return CustomResponse();
        }

        private async Task<bool> IsValidMandatoryInformation(UserRegisterViewModel userRegister)
        {
            var deliveryman = await _deliverymanRepository.GetByCnpjAsync(userRegister.Cnpj);

            if (deliveryman != null)
                AddError("CNPJ already registered");

            deliveryman = await _deliverymanRepository.GetByCnhAsync(userRegister.DriverLicenseNumber);

            if (deliveryman != null)
                AddError("Driver's license already registered");

            return !Erros.Any();
        }

        //[ClaimsAuthorize("Authentication", "Master")]
        //[HttpPost("AddClaims")]
        private async Task<ActionResult> AddClaims(string email, UserClaimViewModel userClaimViewModel)
        {
            var user = await _userManager.FindByNameAsync(email);
            var result = await _userManager.AddClaimAsync(user, new Claim(userClaimViewModel.Type, userClaimViewModel.Value));

            if (result.Succeeded)
                return CustomResponse();

            return CustomResponse();
        }



        private async Task<UserResponseLoginViewModel> GerarToken(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await GetClaimsUser(claims, user);
            var encodedToken = GetToken(identityClaims);

            return GetResponseToken(encodedToken, user, claims);
        }

        private async Task<ClaimsIdentity> GetClaimsUser(ICollection<Claim> claims, IdentityUser user)
        {

            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
                claims.Add(new Claim("role", userRole));


            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }
        private string GetToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidatedOn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private UserResponseLoginViewModel GetResponseToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new UserResponseLoginViewModel
            {
                AcessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };
        }

        private async void SendMessage(object message)
        {
            _rabbitMqPublish.Publish(message, _registerQueueSettings.RoutingKey,
                                              _registerQueueSettings.Queue,
                                              _registerQueueSettings.Exchange);
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
