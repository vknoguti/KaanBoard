using KaanBoard.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KaanBoard.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(ClaimsUserDTO<Guid> claims);
        string GenerateRefreshToken();

        ClaimsPrincipal GetClaimsPrincipal(string token);
        public void SetTokensInsideCookie(TokenDTO tokenDTO, HttpContext context);
    }
}
