using System.ComponentModel;

namespace KaanBoard.Enums
{
    public enum RenewJWTStatus
    {
        [Description("Refresh Token is null")]
        NullRefreshToken,

        [Description("Refresh Token is invalid")]
        InvalidRefreshToken,

        [Description("Renewed Access Token and Refresh Token")]
        Success
    }
}
