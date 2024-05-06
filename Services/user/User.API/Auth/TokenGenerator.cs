using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.API.Interfaces.Auth;
using User.API.ViewModels;
using User.Services.Interfaces;

namespace User.API.Auth
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);
        private readonly IRedisService _redisService;

        public TokenGenerator(IConfiguration configuration, IRedisService redisService)
        {
            _configuration = configuration;
            _redisService = redisService;
        }

        public JwtTokenResult GenerateToken(string email, EnumRole role)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, role.ToString())
            }),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var jToken = jwtToken.CreateToken(tokenDescriptor);
            var wToken = jwtToken.WriteToken(jToken);

            StoreToken(email, wToken);

            return new JwtTokenResult
            {
                Token = wToken,
                Expiration = DateTime.UtcNow.Add(TokenLifetime)
            };

        }

        public bool RemoveToken(string email)
        {
            var existingToken = _redisService.GetValue($"jwt:{email}");

            // Se existir um token associado ao usuário, remove o token antigo
            if (!string.IsNullOrEmpty(existingToken))
            {
                _redisService.RemoveValue($"jwt:{email}");
                return true;
            }
            return false;

        }

        private void StoreToken(string userId, string token)
        {
            // Verifica se já existe um token associado ao usuário
            var existingToken = _redisService.GetValue($"jwt:{userId}");

            // Se existir um token associado ao usuário, remove o token antigo
            if (!string.IsNullOrEmpty(existingToken))
            {
                _redisService.RemoveValue($"jwt:{userId}");
            }

            // Armazena o novo token JWT associado ao ID do usuário no Redis
            _redisService.SetValue($"jwt:{userId}", token);
        }
    }

}
