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
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return await _executor.ExecuteAsync(request);
        }
    }
}
