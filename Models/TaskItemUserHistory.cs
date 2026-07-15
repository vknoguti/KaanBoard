using KaanBoard.Models.Entities;
using KaanBoard.Models.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaanBoard.Models
{
    [Table("task_item_user_history")]
    public class TaskItemUserHistory
    {
        [Column("id_user")]
        public Guid Iduser { get; set; }
        [Column("id_task_item")]
        public Guid IdTaskItem { get; set; }
        [Column("dt_action_date")]
        public DateTimeOffset? ActionDate { get; set; }
        [Column("tx_action")]
        public string? TxAction { get; set; }

        public ApplicationUser User { get; set; } = null!;
        public TaskItem TaskItem { get; set; } = null!;
    }
}
