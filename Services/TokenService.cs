using KaanBoard.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KaanBoard.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly string _secretKey;
        private readonly int _accessTokenExpiryMinutes;
        private readonly int _refreshTokenExpiryMinutes;
        public TokenService(IConfiguration config)
        {
            _config = config;
            _secretKey = config["JWT:SecretKey"] ?? throw new InvalidOperationException("Invalid Secret Key");
            _accessTokenExpiryMinutes = _config.GetSection("JWT").GetValue<int>("AccessTokenExpiryMinutes", 60); 
        }

        public JwtSecurityToken GenerateAccessToken( UserJwtModelDTO userJWT)
        {
            var claims = new[]
            { 
                //Mudar aqui para o enum do tipo de dados que eu usarei aqui
                new Claim("IdUser", new UserService().GetUserId().ToString()),
                new Claim("IdSession", 0.ToString()),
                new Claim(ClaimTypes.Name, userJWT.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credential,
                Expires = DateTime.UtcNow.AddMinutes(_accessTokenExpiryMinutes)
            };

            return new JwtSecurityTokenHandler().CreateJwtSecurityToken(tokenDescriptor);
        }
    }
}
