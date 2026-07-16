using KaanBoard.Data;
using Microsoft.AspNetCore.Mvc;

namespace KaanBoard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Teste : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public Teste(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Boards.ToList());
        }

        
    }
}
