using System.Text.Json.Serialization;

namespace KaanBoard.Entities
{
    public class TaskItemUserHistory
    {
        public Guid Iduser { get; set; }
    
        public Guid IdTaskItem { get; set; }
     
        public DateTimeOffset? ActionDate { get; set; }
    
        public string? TxAction { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public User<Guid> User { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TaskItem TaskItem { get; set; } = null!;
    }
}
