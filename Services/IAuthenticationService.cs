using KaanBoard.DTOs;

namespace KaanBoard.Services
{
    public interface IAuthenticationService
    {
        public Task<RegisterResponse> Register(RegisterUserDTO registerUser);
        public Task<LoginResponse> Login(LoginUserDTO loginUser);
    }
}