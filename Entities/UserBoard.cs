using KaanBoard.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KaanBoard.Entities
{
    public class UserBoard
    {
        //POSTGRESQL
        //[Column(TypeName = "uuid")]
        public Guid IdBoard { get; set; }
        //POSTGRESQL
        //[Column(TypeName = "uuid")]
        public Guid IdUser { get; set; }

        [Column(TypeName = "varchar(40)")]
        public string? FlUserRole { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ApplicationUser<Guid> User { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Board Board { get; set; } = null!;
    }
}
