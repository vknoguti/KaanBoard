using System.ComponentModel;

namespace KaanBoard.Enums
{
    public enum RenewJWTStatus
    {
        [Description("Access Token is null")]
        NullAcessToken,

        [Description("Refresh Token is null")]
        NullRefreshToken,

        [Description("Access Token Invalid")]
        InvalidAccessToken,

        [Description("Refresh Token is invalid")]
        InvalidRefreshToken,

        [Description("Renewed Access Token and Refresh Token")]
        Success
    }
}
