
using KaanBoard.Data;
using KaanBoard.DTOs;
using KaanBoard.DTOs.Mappings;
using KaanBoard.Enums;
using KaanBoard.Services;
using Microsoft.AspNetCore.Mvc;

namespace KaanBoard.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        //REMOVER DEPOIS
        private readonly ApplicationDbContext _context;
        public AuthenticationController(IAuthenticationService authService, ApplicationDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        //private static readonly RegisterUserDTO DefaultRegister = new RegisterUserDTO
        //{
        //    Name = "Um nome qualquer aí",
        //    Email = "email@gmail.com",
        //    UserName = "UserName1",
        //    Password = "Password1*" // Fixed spelling here
        //};
        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            return Ok(_context.Users);
        }

        [HttpPost("Register")]
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO login)
        {
            var loginResponse = await _authService.Login(login);
            
            if(loginResponse.StatusCode == LoginStatus.InvalidCredentials)
            {
                NotFound(loginResponse);
            }

            if(loginResponse.StatusCode == LoginStatus.InvalidCredentials)
            {
                BadRequest(loginResponse);
            }

            return Ok(loginResponse);
        }
    }
}
