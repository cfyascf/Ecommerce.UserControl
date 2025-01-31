using EC_User.FunctionApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace EC_User.FunctionApp.Middlewares
{

public class AuthenticationMiddleware : IFunctionsWorkerMiddleware
{
    private readonly JwtService _jwtService;

    private readonly string[] _pathsToSkip;

    public AuthenticationMiddleware(JwtService jwtService)
    {
        _jwtService = jwtService;
        _pathsToSkip =
        [
            "/api/v1/login"
        ];
    }

    private bool TryGetBearerToken(string auth, out string? token)
    {
        if (auth is not null)
        {
            var parts = auth.Split(" ");
            if (parts.Length == 2 && parts[0] == "Bearer")
            {
                token = parts[1];
                return true;
            }
        }

        token = null;
        return false;
    }

    private async Task RespondWithErrorAsync(HttpContext context, int statusCode, string message)
    {
        var error = new Error(message);
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            bool mustSkip = _pathsToSkip.Contains(
                    context.GetHttpContext()!.Request.Path.Value,
                    StringComparer.OrdinalIgnoreCase);

            if (mustSkip)
            {
                await next.Invoke(context);
                return;
            }

            var auth = context.GetHttpContext()!.Request.Headers.Authorization.FirstOrDefault();
            if(!TryGetBearerToken(auth!, out var token))
            {
                await RespondWithErrorAsync(context.GetHttpContext()!, 400, "Invalid authorization headers!");
                return;
            }

            try 
            {
                _jwtService.ValidateToken(token!);   
            }
            catch(Exception)
            {
                await RespondWithErrorAsync(context.GetHttpContext()!, 401, "Invalid JWT token!");
                return;
            }

            await next.Invoke(context);
        }
    }
}