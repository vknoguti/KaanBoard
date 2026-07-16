using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KaanBoard.Entities
{
    public class TaskItem
    {
        public Guid IdTaskItem {  get; set; }
        
        [Required]
        public Guid IdColumn { get; set; }
        
        [Column(TypeName = "varchar(100)")]
        public string? TxTitle { get; set; }
        
        //POSTGRESQL
        //[Column(TypeName = "text")]
        [Column(TypeName = "nvarchar(500)")]
        public string? TxDescription { get; set;  }

        //QUANDO FOR USAR POSTGRESQL
        //[Column(TypeName = "bool")]
        [Column(TypeName = "bit")]
        public bool? FlCompleted { get; set; }

        //QUANDO FOR USAR POSTGRESQL
        //[Column(TypeName = "timestamptz")]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? DueDate { get; set; }

        [Column(TypeName = "smallint")]
        public int? NrPosition { get; set; }

        //QUANDO FOR USAR POSTGRESQL
        //[Column(TypeName = "timestamptz")]
        [Column(TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Column Column { get; set; } = null!;
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<TaskItemUserHistory> TaskItemHistory = new HashSet<TaskItemUserHistory>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Comment> Comments = new HashSet<Comment>(); 
    }
}
