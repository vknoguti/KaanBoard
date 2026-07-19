using System.ComponentModel.DataAnnotations;

namespace KaanBoard.DTOs
{
    public class LoginRequestDTO
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

    }
}
