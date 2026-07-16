using KaanBoard.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KaanBoard.Entities.Identity
{
    
    public class ApplicationUser<TKey> : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        public override TKey Id { get; set; } = default!;

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public override string? UserName { get; set; } = null!;

        [Column(TypeName = "nvarchar(200)")]
        [Required]
        public string Name { get; set; } = null!;

        [Column(TypeName = "nvarchar(60)")]
        [Required]
        public override string? Email { get => base.Email!; set => base.Email = value; }

        [Column(TypeName = "varchar(255)")]
        public override string? PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }

        [Column(TypeName = "nvarchar(50)")]
        public override string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        [Required]
        [Column(TypeName = "datetimeoffset(7)")]
        //QUANDO FOR USAR POSTGRESQL
        //[Column("dt_created", TypeName = "timestamptz")]
        public DateTimeOffset CreatedAt { get; set; }

        [Column(TypeName = "datetimeoffset(7)")]
        //QUANDO FOR USAR POSTGRESQL
        //[Column("dt_updated", TypeName = "timestamptz")]
        public DateTimeOffset? UpdatedAt { get; set; }

        //QUANDO FOR USAR POSTGRESQL
        //[Column("fl_ativo", TypeName = "bool")]
        [Column(TypeName = "bit")]
        public bool? FlAtivo { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<UserBoard> UserBoards = new HashSet<UserBoard>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<TaskItemUserHistory> TaskItemHistory = new HashSet<TaskItemUserHistory>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Comment> Comments = new HashSet<Comment>();
    }
}
