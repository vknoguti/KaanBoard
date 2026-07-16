using KaanBoard.Data;
using KaanBoard.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KaanBoard.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    public class BoardController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public BoardController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Guid id)
        {
            return Ok(_dbContext.Boards.Include(b => b.Columns).ThenInclude(c => c.TaskItem));
        }
    }
}
