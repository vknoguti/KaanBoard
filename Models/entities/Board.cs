using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaanBoard.Models.entities
{
    [Table("Boards")]
    public class Board
    {
        public Board() { }

        [Key]
        public Guid ID_BOARD { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string? TX_NAME { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string? BACKGROUND_COLOR { get; set;  }
    }
}
