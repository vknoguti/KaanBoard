using KaanBoard.Enums;

namespace KaanBoard.DTOs
{
    public class RefreshTokenResponse : BaseResponse<RenewJWTStatus>
    {
        public TokenDTO? tokenDTO;
    }
}
