using System;
using System.Security.Claims;

namespace KaanBoard.DTOs
{
    public class ClaimsUserDTO<TKey> 
    {
        public TKey IdUser { get; set; } = default!;
        public string UserName { get; set; } = default!;
    }
}