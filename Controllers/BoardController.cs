using KaanBoard.Data;
using KaanBoard.DTOs;
using KaanBoard.Enums;
using KaanBoard.Extensions;
using KaanBoard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KaanBoard.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    public class BoardController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public BoardController(ApplicationDbContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet("get-boards")]
        public IActionResult GetBoards()
        {
            Request.Cookies.TryGetValue(nameof(TokenDTO.AccessToken), out var accessToken);
      
            var principals = _tokenService.GetClaimsPrincipal(accessToken!);

            if (principals is null)
            {
                return BadRequest(new BaseResponse1<object>().GenerateResponse1<object>(AppStatus.PrincipalsNotFound));
            }


            var claimsUserDTO = _tokenService.GetClaimsUserDTO<Guid>(principals);


            return Ok(claimsUserDTO);
        }



        [HttpGet]
        public IActionResult Get([FromQuery] Guid id)
        {
            return Ok(_dbContext.Boards.Include(b => b.Columns).ThenInclude(c => c.TaskItem));
        }
    }
}
