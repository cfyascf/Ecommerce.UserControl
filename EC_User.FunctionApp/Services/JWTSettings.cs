namespace EC_User.FunctionApp.Services
{
    public record JwtSettings
    {
        public required string SecretKey { get; init; }
    }
}