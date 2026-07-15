using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaanBoard.Models.Entities
{
    [Table("task_item")]
    public class TaskItem
    {
        [Column("id_task_item")]
        public Guid IdTaskItem {  get; set; }
        
        [Column("id_column")]
        [Required]
        public Guid IdColumn { get; set; }
        
        [Column("tx_title", TypeName = "varchar(100)")]
        public string? TxTitle { get; set; }
        
        //POSTGRESQL
        //[Column("tx_description", TypeName = "text")]
        [Column("tx_description", TypeName = "nvarchar(500)")]
        public string? TxDescription { get; set;  }

        //QUANDO FOR USAR POSTGRESQL
        //[Column("fl_completed", TypeName = "bool")]
        [Column("fl_completed", TypeName = "bit")]
        public bool? FlCompleted { get; set; }

        //QUANDO FOR USAR POSTGRESQL
        //[Column("dt_due_date", TypeName = "timestamptz")]
        [Column("dt_due_date", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? DueDate { get; set; }

        [Column("nr_position", TypeName = "smallint")]
        public int? NrPosition { get; set; }

        //QUANDO FOR USAR POSTGRESQL
        //[Column("dt_updated_at", TypeName = "timestamptz")]
        [Column("dt_updated_at", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? UpdatedAt { get; set; }

        public Column Column { get; set; } = null!;

        public ICollection<TaskItemUserHistory> TaskItemHistory = new HashSet<TaskItemUserHistory>();

        public ICollection<Comment> Comments = new HashSet<Comment>(); 
    }
}
