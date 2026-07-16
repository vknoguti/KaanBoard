using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KaanBoard.Entities
{
    public class Column
    {
        public Guid IdColumn { get; set; }

        public Guid? IdBoard { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? TxTitle { get; set; }

        [Column(TypeName = "smallint")]
        public int? Nrposition { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Board? Board { get; set; } = null;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<TaskItem> TaskItem { get; set; } = new HashSet<TaskItem>();
    }
}
