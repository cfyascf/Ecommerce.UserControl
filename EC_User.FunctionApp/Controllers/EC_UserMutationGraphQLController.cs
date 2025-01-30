using EC_User.FunctionApp.Models;
using EC_User.FunctionApp.Services;
using EC_User.Domain.Entities;

namespace EC_User.FunctionApp.Controllers
{
    public class EC_UserMutationGraphQLController
    {
        private readonly UserService _userService;
        private readonly CharacterService _characterService;

        public EC_UserMutationGraphQLController(UserService userService, CharacterService characterService)
        {
            _userService = userService;
            _characterService = characterService;
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

        public async Task<Character?> CreateCharacter()
        {
            return await _characterService.SaveApiCharacter();
        }
    }
}