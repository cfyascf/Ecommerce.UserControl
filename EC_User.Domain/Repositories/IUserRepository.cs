using EC_User.Domain.Entities;

namespace EC_User.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(User user);
        public Task<User> DeleteUser(User user);
        public IQueryable<User> GetUsers();
    }
}