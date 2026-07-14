using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaanBoard.Models.Entities.Identity
{
    
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Column("tx_name", TypeName = "varchar(200)")]
        [Required]
        public string Name { get; set; } = null!;

        [Column("tx_email", TypeName = "varchar(60)")]
        [Required]
        public override string? Email { get => base.Email; set => base.Email = value; }

        [Required]
        [Column("dt_created", TypeName = "datetimeoffset(7)")]
        //QUANDO FOR USAR POSTGRESQL
        //[Column("dt_created", TypeName = "timestamptz")]
        public DateTimeOffset CreatedAt { get; set; }

        [Column("dt_updated", TypeName = "datetimeoffset(7)")]
        //QUANDO FOR USAR POSTGRESQL
        //[Column("dt_updated", TypeName = "timestamptz")]
        public DateTimeOffset? UpdatedAt { get; set; }

        //QUANDO FOR USAR POSTGRESQL
        //[Column("fl_ativo", TypeName = "bool")]
        [Column("fl_ativo", TypeName = "bit")]
        public bool? FlAtivo { get; set; }

        public ICollection<UserBoard> UserBoards = new HashSet<UserBoard>();
    }
}
