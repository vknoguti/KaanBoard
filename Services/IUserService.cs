namespace KaanBoard.Services
{
    public interface IUserService
    {
        public Guid GetUserId();
        public Guid GetSessionId();
    }
}
