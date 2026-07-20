using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KaanBoard.Entities
{
    public class Comment
    {
        public Guid IdComment { get; set;  }

        public Guid? IdUser { get; set; }

        public Guid? IdTaskItem { get; set; }

        //POSTGRESQL
        //colocar text aqui
        [Column(TypeName = "varchar(500)")]
        public string? TxComment { get; set; }

        //POSTGRESQL
        //colocar text aqui
        [Column(TypeName = "varchar(200)")]
        public string? TxEmojis { get; set;  }

        //QUANDO FOR USAR POSTGRESQL
        //[Column(TypeName = "timestamptz")]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset DtCreation { get; set; }

        //QUANDO FOR USAR POSTGRESQL
        //[Column(TypeName = "timestamptz")]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? DtLastModified { get; set; }

        public bool IsDeleted { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public User<Guid> User { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TaskItem TaskItem { get; set; } = null!;

    }
}
