using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KaanBoard.Entities
{
    public class Board
    {
        public Guid IdBoard { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? Name { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string? Background_Color { get; set;  }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<UserBoard> UserBoards { get; set; } = new List<UserBoard>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Column> Columns { get; set; } = new List<Column>();
    }
}
