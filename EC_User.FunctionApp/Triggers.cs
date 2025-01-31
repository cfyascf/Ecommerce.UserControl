using EC_User.FunctionApp.Controllers;
using EC_User.FunctionApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace EC_User.FunctionApp
{
    public class Triggers
    {
        private readonly IGraphQLRequestExecutor _executor;
        private readonly ILogger<Triggers> _logger;

        public Triggers(IGraphQLRequestExecutor executor, ILogger<Triggers> logger)
        {
            _executor = executor;
            _logger = logger;
        }

        [Function("GraphQLHttpTrigger")]
        public async Task<IActionResult> RunGraphQL([HttpTrigger(
            AuthorizationLevel.Anonymous, "get", "post")] 
            HttpRequest request)
        {
            _logger.LogInformation(nameof(EC_UserGraphQLController));
            return await _executor.ExecuteAsync(request);
        }

        // [Function("SaveCharacterOnDb")]
        // public async Task SaveCharacterOnDb(
        //     [TimerTrigger("0 */5 * * * *")] TimerInfo timer,
        //     [Service] CharacterService service)
        // {
        //     _logger.LogInformation("Character saved on DB.");
        //     await service.SaveApiCharacter();
        // }
    }
}
