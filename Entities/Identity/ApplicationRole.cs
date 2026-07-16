using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaanBoard.Entities.Identity
{
    public class ApplicationRole<TKey> : IdentityRole<TKey> where TKey : IEquatable<TKey>
    {
        public override TKey Id { get; set; } = default!;

        [Column(TypeName = "nvarchar(200)")]
        public override string? Name { get; set; }
    }
}
