using KaanBoard.Enums;
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

        public BoardRole UserRole { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public User<Guid> User { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Board Board { get; set; } = null!;
    }
}
