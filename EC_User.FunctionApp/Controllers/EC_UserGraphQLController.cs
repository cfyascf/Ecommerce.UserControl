using EC_User.FunctionApp.Services;
using EC_User.Domain.Entities;

namespace EC_User.FunctionApp.Controllers
{
    public class EC_UserGraphQLController
    {
         private readonly UserService _userService;

        public EC_UserGraphQLController(UserService userService)
        {
            _userService = userService;
        }
        public async Task<List<User>> GetUser(string search)
        {
            return await _userService.GetUsers(search);
        }
    }
}