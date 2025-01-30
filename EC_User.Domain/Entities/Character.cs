using System.ComponentModel.DataAnnotations.Schema;

namespace EC_User.Domain.Entities
{
    [Table("Character", Schema = "EC_User")]
    public class Character
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
    }
}