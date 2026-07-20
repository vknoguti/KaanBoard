using KaanBoard.Entities;

namespace KaanBoard.DTOs.Mappings
{
    public static class ApplicationUserDTOMappingExtensions
    {
        public static User<TKey>? ToApplicationUser<TKey>(this RegisterUserDTO registerUser) where TKey : IEquatable<TKey>
        {
            if (registerUser == null) return null;
            var applicationUser = new User<TKey>
            {
                UserName = registerUser.UserName,
                Name = registerUser.Name,
                Email = registerUser.Email,
                PasswordHash = registerUser.PasswordHash,
                PhoneNumber = registerUser.PhoneNumber
            };
            return applicationUser;
        }
    }
}
