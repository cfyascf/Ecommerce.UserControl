using EC_User.Domain.Entities;
using EC_User.Domain.Repositories;
using EC_User.Infrastructure.Contexts;

namespace EC_User.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EC_UserContext _context;

        public UserRepository(EC_UserContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUser(User user)
        {
            var register = _context.Users.FirstOrDefault(u => u.Email == user.Email);

            if(register == null) 
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }

            return null!;
        }

        public async Task<User> DeleteUser(long id)
        {
            var register = await _context.Users.FindAsync(id);

            if(register != null) 
            {
                _context.Users.Remove(register);
                await _context.SaveChangesAsync();

                return register;
            }

            return null!;
        }

        public IQueryable<User> GetUsers()
        {
            return _context.Users;
        }

        public async Task<User> UpdateUser(User user)
        {
            var register = await _context.Users.FindAsync(user.Id);
            if(register != null)
            {
                register.Fullname = user.Fullname; 
                register.Email = user.Email;
                register.BirthDate = user.BirthDate;
                register.Password = user.Password;

                await _context.SaveChangesAsync();

                return register;
            }

            return null!;
        }
    }
}