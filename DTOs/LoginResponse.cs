using KaanBoard.Enums;

namespace KaanBoard.DTOs
{
    public class LoginResponse : BaseResponse<LoginStatus>
    {
        public TokenDTO? tokenDTO { get; set; }
    }
}
