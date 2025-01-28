using System.ComponentModel.DataAnnotations.Schema;

namespace EC_User.Domain.Entities
{
    [Table("User", Schema = "EC_User")]
    public class User 
    {
        public long Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; } = DateOnly.MinValue;
        public string Password { get; set; } = string.Empty;
    }
}