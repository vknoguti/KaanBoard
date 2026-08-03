using KaanBoard.Enums;
using System.Net.NetworkInformation;

namespace KaanBoard.DTOs
{
    public class BaseResponse1<TData> 
    {
        public AppStatus StatusCode { get; set; }
        public string? StatusName { get; set; } = string.Empty;
        public string? Message { get; set; } = string.Empty;
        public TData? Data { get; set; }
    }
}
