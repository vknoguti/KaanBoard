
using KaanBoard.Data;
using KaanBoard.DTOs;
using KaanBoard.DTOs.Mappings;
using KaanBoard.Enums;
using KaanBoard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KaanBoard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly ITokenService _tokenService;
        //REMOVER DEPOIS
        private readonly ApplicationDbContext _context;
        public AuthenticationController(IAuthenticationService authService, ApplicationDbContext context, ITokenService tokenService)
        {
            _authService = authService;
            _context = context;
            _tokenService = tokenService;
        }

        [HttpGet("TesteClaims")]
        public IActionResult TestandoClaims()
        {
            Request.Cookies.TryGetValue(nameof(TokenDTO.AccessToken), out var accessToken);
            Request.Cookies.TryGetValue(nameof(TokenDTO.RefreshToken), out var refreshToken);
            return Ok(new { accessToken, refreshToken });
        }


        //private static readonly RegisterUserDTO DefaultRegister = new RegisterUserDTO
        //{
        //    Name = "Um nome qualquer aí",
        //    Email = "email@gmail.com",
        //    UserName = "UserName1",
        //    Password = "Password1*" // Fixed spelling here
        //};
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            return Ok(_context.Users);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RenewTokenJWT()
        {
            Request.Cookies.TryGetValue(nameof(TokenDTO.AccessToken), out var accessToken);
            Request.Cookies.TryGetValue(nameof(TokenDTO.RefreshToken), out var refreshToken);
            if (accessToken is null || refreshToken is null)
            {
                return BadRequest("Invalid Cookie");
            }

            var renewResponse = await _authService.RenewJWTWithRefreshToken(
                new TokenDTO { AccessToken = accessToken, RefreshToken = refreshToken });

            if(renewResponse.StatusCode == RenewJWTStatus.Success)
            {
                return Ok(renewResponse);
            }

            if(renewResponse.StatusCode == RenewJWTStatus.NullAcessToken 
                || renewResponse.StatusCode == RenewJWTStatus.NullRefreshToken 
                || renewResponse.StatusCode == RenewJWTStatus.InvalidAccessToken 
                || renewResponse.StatusCode == RenewJWTStatus.InvalidRefreshToken)
            {
                return BadRequest(renewResponse);
            }

            return Ok(renewResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
            }

            var registerResponse = await _authService.Register(registerUser);
            if(registerResponse.StatusCode == RegisterStatus.UsernameAlreadyExists)
            {
                return BadRequest(registerResponse);
            }
            if(registerResponse.StatusCode == RegisterStatus.EmailAlreadyExists)
            {
                return BadRequest(registerResponse);
            }
            return Ok(registerResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO login)
        {
            LoginResponse loginResponse = await _authService.Login(login);
        
            if(loginResponse.StatusCode == LoginStatus.NotFound)
            {
                return NotFound(loginResponse);
            }

            if(loginResponse.StatusCode == LoginStatus.InvalidCredentials)
            {
                return BadRequest(loginResponse);
            }

            var tokenDTO = loginResponse.tokenDTO;

            this.Response.Cookies.Append(nameof(TokenDTO.AccessToken), tokenDTO!.AccessToken,
                new CookieOptions
                {
                    Expires = tokenDTO?.AcessTokenExpiresAt,
                    //MUDAR AQUI
                    HttpOnly = false,
                    IsEssential = true,
                    Secure = true,
                    //MUDAR AQUI
                    SameSite = SameSiteMode.None
                });

            this.Response.Cookies.Append(nameof(TokenDTO.RefreshToken), tokenDTO!.RefreshToken,
                new CookieOptions
                {
                    Expires = tokenDTO?.RefreshTokenExpiresAt,
                    //MUDAR AQUI
                    HttpOnly = false,
                    IsEssential = true,
                    Secure = true,
                    //MUDAR AQUI
                    SameSite = SameSiteMode.None
                });
            return Ok(loginResponse);
        }
    }
}
