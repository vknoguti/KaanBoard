using KaanBoard.Data;
using KaanBoard.DTOs;
using KaanBoard.DTOs.Mappings;
using KaanBoard.Entities;
using KaanBoard.Enums;
using KaanBoard.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            var accessToken = _tokenService.GenerateAccessToken(targetUser.ToClaimsUser<Guid>());
            var refreshToken = _tokenService.GenerateRefreshToken();

            //TEORICAMENTE MEXENDO NO BANCO DO REDIS
            targetUser.RefreshToken = refreshToken;
            targetUser.RefreshTokenExpiryDate = _tokenService.RefreshTokenExpirationDate();
            await _context.SaveChangesAsync();
            
            response.tokenDTO = new TokenDTO
            {
                AccessToken = accessToken,
                AcessTokenExpiresAt = _tokenService.AccessTokenExpirationDate(),
                RefreshToken = refreshToken,
                RefreshTokenExpiresAt = _tokenService.RefreshTokenExpirationDate()
            };
            return response;
        }

        public async Task<RefreshTokenResponse> RenewJWTWithRefreshToken(TokenDTO tokenDTO)
        {
            var response = new RefreshTokenResponse();
            if (tokenDTO.AccessToken is null)
            {
                return (RefreshTokenResponse)response.GenerateResponse<RenewJWTStatus>(RenewJWTStatus.NullAcessToken);
            }
            if (tokenDTO.RefreshToken is null)
            {
                return (RefreshTokenResponse)response.GenerateResponse<RenewJWTStatus>(RenewJWTStatus.NullRefreshToken);
            }

            ClaimsPrincipal? principals = null;
            try
            {
                principals = _tokenService.GetClaimsPrincipal(tokenDTO.AccessToken);
            }
            catch (Exception e)
            {
                return (RefreshTokenResponse)response.GenerateResponse<RenewJWTStatus>(RenewJWTStatus.InvalidAccessToken);
            }

            var claimsUser = principals.ToClaimsUserDTO<Guid>();
            if (claimsUser is null)
            {
                return (RefreshTokenResponse)response.GenerateResponse<RenewJWTStatus>(RenewJWTStatus.InvalidAccessToken);
            }

            var targetUser = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == claimsUser.IdUser && u.UserName == claimsUser.UserName);

            if (targetUser is null || targetUser.RefreshToken != tokenDTO.RefreshToken || targetUser.RefreshTokenExpiryDate < DateTimeOffset.UtcNow)
            {
                return (RefreshTokenResponse)response.GenerateResponse<RenewJWTStatus>(RenewJWTStatus.InvalidRefreshToken);
            }

            response = (RefreshTokenResponse)response.GenerateResponse<RenewJWTStatus>(RenewJWTStatus.Success);
            var accessToken = _tokenService.GenerateAccessToken(targetUser.ToClaimsUser<Guid>());
            var refreshToken = _tokenService.GenerateRefreshToken();

            //TEORICAMENTE MEXENDO NO BANCO DO REDIS
            targetUser.RefreshToken = refreshToken;
            targetUser.RefreshTokenExpiryDate = _tokenService.RefreshTokenExpirationDate();
            await _context.SaveChangesAsync();

            response.tokenDTO = new TokenDTO
            {
                AccessToken = accessToken,
                AcessTokenExpiresAt = _tokenService.AccessTokenExpirationDate(),
                RefreshToken = refreshToken,
                RefreshTokenExpiresAt = _tokenService.RefreshTokenExpirationDate()
            };
            return response;
        }
    }
}
