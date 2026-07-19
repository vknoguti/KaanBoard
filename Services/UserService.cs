
namespace KaanBoard.Services
{
    public class UserService : IUserService
    {
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
