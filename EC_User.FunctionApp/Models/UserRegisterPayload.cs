namespace EC_User.FunctionApp.Models
{
    public class UserRegisterPayload
    {
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; } = DateOnly.MinValue;
        public string Password { get; set; } = string.Empty;
    }
}
