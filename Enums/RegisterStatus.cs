using System.ComponentModel;

namespace KaanBoard.Enums
{
    public enum RegisterStatus
    {
        [Description("Registration successful")]
        Success,

        [Description("Email is already registered")]
        EmailAlreadyExists,

        [Description("Username is already taken")]
        UsernameAlreadyExists,

        [Description("Password does not meet complexity requirements")]
        WeakPassword,

        [Description("Provided data is invalid")]
        InvalidData,

        [Description("Registration failed due to an unexpected error")]
        Failed
    }
}
