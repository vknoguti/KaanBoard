using KaanBoard.Data;
using KaanBoard.DTOs;
using KaanBoard.DTOs.Mappings;
using KaanBoard.Entities;
using KaanBoard.Enums;
using KaanBoard.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace KaanBoard.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User<Guid>> _passwordHasher;
        private readonly ITokenService _tokenService;
        public AuthenticationService(ApplicationDbContext context, IPasswordHasher<User<Guid>> passwordHasher, ITokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<RegisterResponse> Register(RegisterUserDTO userRegister)
        {
            var registerResponse = new RegisterResponse();
            var queryUser =  _context.Users.AsQueryable().AsNoTracking();
            var userNameFound = await queryUser.AnyAsync(u => u.UserName == userRegister.UserName);
            if (userNameFound)
            {
                return (RegisterResponse)registerResponse.GenerateResponse<RegisterStatus>(RegisterStatus.UsernameAlreadyExists);
            } 
            
            var emailFound = await queryUser.AnyAsync(u => u.Email == userRegister.Email);
            if (emailFound)
            {
                return (RegisterResponse)registerResponse.GenerateResponse<RegisterStatus>(RegisterStatus.EmailAlreadyExists);
            }

            var userMapped = new User<Guid>();
            userMapped = userRegister.ToUser<Guid>(_passwordHasher.HashPassword(userMapped, userRegister.Password)) ?? 
                throw new ArgumentNullException(nameof(userRegister), "The mapping value was null");

            await _context.Users.AddAsync(userMapped);
            var success = await _context.SaveChangesAsync();
            
            if(success <= 0)
            {
                return (RegisterResponse)registerResponse.GenerateResponse<RegisterStatus>(RegisterStatus.Failed);
            }

            return (RegisterResponse)registerResponse.GenerateResponse<RegisterStatus>(RegisterStatus.Success);
        }

        public async Task<LoginResponse> Login(LoginUserDTO loginUser)
        {
            var loginResponse = new LoginResponse();

            var targetUser = await _context.Users.FirstOrDefaultAsync(t => t.UserName == loginUser.UserName);
            if (targetUser is null)
            {
                return (LoginResponse)loginResponse.GenerateResponse<LoginStatus>(LoginStatus.NotFound);
            }
            
            var matchPassword = _passwordHasher.VerifyHashedPassword(targetUser, targetUser.PasswordHash, loginUser.Password);
            if(matchPassword != PasswordVerificationResult.Success)
            {
                return (LoginResponse)loginResponse.GenerateResponse<LoginStatus>(LoginStatus.InvalidCredentials);
            }

            var response = (LoginResponse)loginResponse.GenerateResponse<LoginStatus>(LoginStatus.Success);
            var acessToken = _tokenService.GenerateAccessToken(targetUser.ToClaimsUser<Guid>());
            var acessTokenExpiration = _tokenService.GetClaimsPrincipal(acessToken).Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);


            response.tokenDTO = new TokenDTO
            {
                AccessToken = acessToken,
                AcessTokenExpiresAt = _tokenService.AccessTokenExpirationDate(),
                RefreshToken = _tokenService.GenerateRefreshToken(),
                RefreshTokenExpiresAt = _tokenService.RefreshTokenExpirationDate()
            };
            return response;
        }
    }
}
