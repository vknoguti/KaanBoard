using KaanBoard.Models.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaanBoard.Models.Entities
{
    public class Comment
    {
        [Column("id_comment")]
        public Guid IdComment { get; set;  }

        [Column("id_user")]
        public Guid? IdUser { get; set; }

        [Column("id_task_item")]
        public Guid? IdTaskItem { get; set; }

        //POSTGRESQL
        //colocar text aqui
        [Column("tx_comment", TypeName = "varchar(500)")]
        public string? TxComment { get; set; }

        //POSTGRESQL
        //colocar text aqui
        [Column("tx_emojis", TypeName = "varchar(200)")]
        public string? TxEmojis { get; set;  }

        //QUANDO FOR USAR POSTGRESQL
        //[Column("dt_creation", TypeName = "timestamptz")]
        [Column("dt_creation", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset DtCreation { get; set; }

        //QUANDO FOR USAR POSTGRESQL
        //[Column("dt_last_modified", TypeName = "timestamptz")]
        [Column("dt_last_modified", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? DtLastModified { get; set; }

        [Column("fl_deleted")]
        public bool IsDeleted { get; set; }

        public ApplicationUser User { get; set; } = null!;
        public TaskItem TaskItem { get; set; } = null!;

    }
}
