using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace KaanBoard.Models.Entities
{
    public class CommentHistory
    {
        [Column("id_comment_history")]
        public Guid IdCommentHistory { get; set;  }

        [Column("id_user")]
        public Guid IdUser { get; set; }

        [Column("id_comment")]
        public Guid IdComment { get; set; }

        //QUANDO FOR USAR POSTGRESQL
        //[Column("dt_action", TypeName = "timestamptz")]
        [Column("dt_action", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset DtAction { get; set; }

        //POSTGRESQL
        //[Column("tx_action", TypeName = "text")]
        [Column("tx_action", TypeName = "varchar(max)")]
        public string? TxAction { get; set; }

    }
}
