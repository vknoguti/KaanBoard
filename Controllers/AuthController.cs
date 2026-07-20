
using KaanBoard.DTOs;
using KaanBoard.DTOs.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace KaanBoard.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        //private static readonly RegisterUserDTO DefaultRegister = new RegisterUserDTO
        //{
        //    Name = "Um nome qualquer aí",
        //    Email = "email@gmail.com",
        //    UserName = "UserName1",
        //    Password = "Password1*" // Fixed spelling here
        //};
        //[HttpPost("Register")]
        //public async Task<IActionResult> Register(RegisterUserDTO? registerUser)
        //{
        //    registerUser = DefaultRegister;
        //    TryValidateModel(registerUser);

        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return BadRequest(ModelState);
        //    //}

        //    var registerUserMapped = registerUser.ToApplicationUser<Guid>();
        //    if(registerUserMapped is null)
        //    {
        //        return BadRequest();
        //    }

        //    var result = await _userManager.CreateAsync(registerUserMapped, registerUser.Password);
        //    if (!result.Succeeded)
        //    {
        //        var errors = string.Join(" ", result.Errors.Select(e => e.Description));
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response
        //        {
        //            Status = "Error",
        //            Message = $"User creation failed! Errors: {errors}"
        //        });
        //    }

        //    return Ok("User creation successfull");
        //}
        //[HttpGet]
        //public IActionResult Login(LoginUserDTO login)
        //{
        //    return Ok();
        //}
    }
}
