using KaanBoard.Data;
using KaanBoard.DTOs;
using KaanBoard.DTOs.Mappings;
using KaanBoard.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KaanBoard.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User<Guid>> _passwordHaser;
        public AuthService(ApplicationDbContext context, IPasswordHasher<User<Guid>> passwordHasher)
        {
            _context = context;
            _passwordHaser = passwordHasher; 
        }

        public async Task<bool> Register(RegisterUserDTO userRegister)
        {

            var userMapped = userRegister.ToApplicationUser<Guid>();
            if (userMapped is null) throw new ArgumentNullException(nameof(userMapped), "The mapping value was null");
            
            await _context.Users.AddAsync(userMapped);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Login(LoginUserDTO loginUser)
        {
            var userSearch = await _context.Users.FirstAsync(t => t.UserName == loginUser.UserName);
            if (userSearch is null) return false;

            var hashUserSearch = userSearch.PasswordHash;

            
        }
    }
}
