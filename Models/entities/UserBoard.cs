using KaanBoard.Models.entities;
using KaanBoard.Models.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaanBoard.Models.Entities
{
    [Table("user_board")]
    public class UserBoard
    {
        //POSTGRESQL
        //[Column("id_board", TypeName = "uuid")]
        [Column("id_board")]
        public Guid IdBoard { get; set; }
        //POSTGRESQL
        //[Column("id_user", TypeName = "uuid")]
        [Column("id_user")]
        public Guid IdUser { get; set; }

        [Column("fl_user_role", TypeName = "varchar(40)")]
        public string? FlUserRole { get; set; }

        public ApplicationUser User { get; set; } = null!;
        public Board Board { get; set; } = null!;
    }
}
