using EC_User.Domain.Entities;
using EC_User.Domain.Repositories;
using EC_User.Infrastructure.Contexts;

namespace EC_User.Infrastructure.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly EC_UserContext _context;
        public CharacterRepository(EC_UserContext context)
        {
            _context = context;
        }
        public async Task<Character> CreateCharacter(Character character)
        {
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();

            return character;
        }
    }
}