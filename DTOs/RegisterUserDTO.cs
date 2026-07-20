using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaanBoard.DTOs
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = nameof(UserName) + " is required")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = nameof(PasswordHash) + " is required")]
        public string PasswordHash { get; set; } = null!;

        [Required(ErrorMessage = nameof(Name) + " is required")]
        public string Name { get; set; } = null!;

        [EmailAddress]
        [Required(ErrorMessage = nameof(Email) + " is required")]
        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }
    }
}
