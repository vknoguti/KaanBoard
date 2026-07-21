using System.ComponentModel;

namespace KaanBoard.Enums
{
    public enum LoginStatus
    {
        [Description("Login successful")]
        Success,

        [Description("User not found")]
        NotFound,

        [Description("Invalid username or password")]
        InvalidCredentials,

        [Description("Account is locked")]
        AccountLocked,
        [Description("Account was suspended")]
        AccountSuspended,
        [Description("Email is not verified")]
        EmailNotVerified
    }
}
