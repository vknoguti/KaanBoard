
using KaanBoard.Data;
using KaanBoard.Entities;

namespace KaanBoard.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Guid GetUserId()
        {
            return Guid.NewGuid();
        }

        public Guid GetSessionId()
        {
            return Guid.NewGuid();
        }
    }
}
