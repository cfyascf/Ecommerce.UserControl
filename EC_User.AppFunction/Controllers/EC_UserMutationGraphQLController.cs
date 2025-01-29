using EC_User.AppFunction.Models;
using EC_User.AppFunction.Services;
using EC_User.Domain.Entities;

namespace EC_User.AppFunction.Controllers
{
    public class EC_UserMutationGraphQLController
    {
        private readonly UserService _userService;

        public EC_UserMutationGraphQLController(UserService userService)
        {
            _userService = userService;
        }
        public async Task<User?> CreateUser(UserRegisterPayload payload)
        {
            return await _userService.CreateUser(payload);
        }

        public async Task<User?> UpdateUser(User payload)
        {
            return await _userService.UpdateUser(payload);
        }

        public async Task<User?> DeleteUser(long id)
        {
            return await _userService.DeleteUser(id);
        }
    }
}