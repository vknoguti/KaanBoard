using KaanBoard.DTOs;

namespace KaanBoard.Extensions
{
    public static class AuthenticationResponseExtension
    {
        public static BaseResponse<TStatus> GenerateResponse<TStatus>(this BaseResponse<TStatus> response, TStatus status) where TStatus : Enum
        {
            response.StatusCode = status;
            response.StatusName = status.ToString();
            response.Message = status.GetDescriptionMessage();
            return response;
        }
    }
}
