namespace KaanBoard.DTOs
{
    public class BaseResponse<TStatus> where TStatus : Enum
    {
        public TStatus? StatusCode { get; set; }
        public string? StatusName { get; set; } = string.Empty;
        public string? Message { get; set; } = string.Empty;
    }
}
