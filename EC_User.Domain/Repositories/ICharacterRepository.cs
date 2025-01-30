using EC_User.Domain.Entities;

namespace EC_User.Domain.Repositories
{
    public interface ICharacterRepository
    {
        public Task<Character> CreateCharacter(Character character);
    }
}