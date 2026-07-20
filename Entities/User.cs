using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace KaanBoard.Entities
{
    public class User<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string UserName { get; set; } = null!;

        [Column(TypeName = "nvarchar(200)")]
        [Required]
        public string Name { get; set; } = null!;

        [Column(TypeName = "nvarchar(60)")]
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string PasswordHash { get; set; } = null!;

        [Column(TypeName = "nvarchar(50)")]
        public string? PhoneNumber { get; set; }

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
