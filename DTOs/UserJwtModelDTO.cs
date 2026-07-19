namespace KaanBoard.DTOs
{
    public class UserJwtModelDTO
    {
        public Guid IdUser { get; set; }
        public Guid? IdSession { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
