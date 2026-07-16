using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace KaanBoard.Entities
{
    public class CommentHistory
    {
        public Guid IdCommentHistory { get; set;  }

        public Guid IdUser { get; set; }

        public Guid IdComment { get; set; }

        //QUANDO FOR USAR POSTGRESQL
        //[Column(TypeName = "timestamptz")]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset DtAction { get; set; }

        //POSTGRESQL
        //[Column(TypeName = "text")]
        [Column(TypeName = "varchar(max)")]
        public string? TxAction { get; set; }

    }
}
