using KaanBoard.Data;
using KaanBoard.DTOs;
using KaanBoard.DTOs.Mappings;
using KaanBoard.Entities;
using KaanBoard.Enums;
using KaanBoard.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KaanBoard.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User<Guid>> _passwordHasher;
        public AuthenticationService(ApplicationDbContext context, IPasswordHasher<User<Guid>> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher; 
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

            var targetUser = await _context.Users.FirstAsync(t => t.UserName == loginUser.UserName);
            if (targetUser is null)
            {
                return (LoginResponse)loginResponse.GenerateResponse<LoginStatus>(LoginStatus.NotFound);
            }
            
            var matchPassword = _passwordHasher.VerifyHashedPassword(targetUser, targetUser.PasswordHash, loginUser.Password);
            if(matchPassword != PasswordVerificationResult.Success)
            {
                return (LoginResponse)loginResponse.GenerateResponse<LoginStatus>(LoginStatus.InvalidCredentials);
            }

            return (LoginResponse)loginResponse.GenerateResponse<LoginStatus>(LoginStatus.Success);
        }
    }
}
