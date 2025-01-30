using EC_User.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EC_User.Infrastructure.Contexts
{
    public class EC_UserContext : DbContext
    {
        public EC_UserContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
    }
}