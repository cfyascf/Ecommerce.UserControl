using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EC_User.FunctionApp.Services
{
public class JwtService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly SymmetricSecurityKey _securityKey;
    private readonly SigningCredentials _credentials;
    private readonly UserContext _userContext;

    public JwtService(
        IServiceProvider serviceProvider,
        JwtSecurityTokenHandler tokenHandler,
        UserContext userContext,
        JwtSettings settings)
    {
        _serviceProvider = serviceProvider;
        _tokenHandler = tokenHandler;
        _userContext = userContext;

        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));

        _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512);
    }

    public void ValidateToken(string jwt)
    {
        ClaimsPrincipal? claims;

        try
        {
            claims = _tokenHandler.ValidateToken(jwt,
                    new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidIssuer = "SkillHub",
                        IssuerSigningKey = _securityKey
                    },
                    out var validatedToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException();
        }

        var userPosition = claims.FindFirst("PermissionLevel")!.Value;

        _userContext.Fill(new ContextData
        {
            UserId = int.Parse(claims.FindFirst("UserId")!.Value),
            Name = claims.FindFirst("Name")!.Value,
            PermissionLevel = Enum.Parse<EPermissionLevel>(userPosition)
        });
    }
}
}