namespace EC_User.ApiClient.Models
{
    public record ApiCharacter(
        string Name,
        string Status,
        string Species,
        string Type,
        string Gender
    );

    public record Results(
        ApiCharacter[] ApiCharacters
    );

    public record Characters(
        Results Results
    );
}