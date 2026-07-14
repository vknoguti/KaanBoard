using KaanBoard.Models.entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaanBoard.Models.Entities
{
    [Table("columns")]
    public class Column
    {
        //POSTGRESQL
        //[Column("id_column", TypeName = "uuid")]
        [Column("id_column")]
        public Guid IdColumn { get; set; }

        [Column("id_board")]
        public Guid? IdBoard { get; set; }

        [Column("tx_title", TypeName = "varchar(100)")]
        public string? TxTitle { get; set; }

        [Column("nr_position", TypeName = "smallint")]
        public int? Nrposition { get; set; }

        public Board? Board { get; set; } = null;
        public ICollection<TaskItem> TaskItem { get; set; } = new HashSet<TaskItem>();
    }
}
