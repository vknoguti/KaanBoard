using KaanBoard.Entities;

namespace KaanBoard.DTOs.Mappings
{
    public static class UserDTOMappingExtensions
    {
        public static User<TKey>? ToUser<TKey>(this RegisterUserDTO registerUser, string passwordHash) where TKey : IEquatable<TKey>
        {
            if (registerUser == null) return null;
            var user = new User<TKey>
            {
                UserName = registerUser.UserName,
                PasswordHash = passwordHash,
                Name = registerUser.Name,
                Email = registerUser.Email,
                PhoneNumber = registerUser.PhoneNumber
            };
            return user;
        }

        //public static RegisterUserDTO ToRegisterDTO<TKey>(this User<TKey> user) where TKey: IEquatable<TKey>
        //{
        //    var registerUser = new RegisterUserDTO
        //    {
        //        Email = user.Email,
        //        Name = user.Name,
        //        PasswordHash = user.PasswordHash,
        //        PhoneNumber = user.PhoneNumber,
        //        UserName = user.UserName
        //    };
        //    return registerUser;
        //}
    }
}
