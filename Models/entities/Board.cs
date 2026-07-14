using KaanBoard.Models.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaanBoard.Models.entities
{
    [Table("boards")]
    public class Board
    {
        [Column("id_board")]
        public Guid IdBoard { get; set; }

        [Column("tx_name", TypeName = "varchar(200)")]
        public string? Name { get; set; }

        [Column("background_color", TypeName = "varchar(30)")]
        public string? Background_Color { get; set;  }

        public ICollection<UserBoard> UserBoards { get; set; } = new List<UserBoard>();

        public ICollection<Column> Columns { get; set; } = new List<Column>();
    }
}
