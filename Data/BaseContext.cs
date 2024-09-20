using Microsoft.EntityFrameworkCore;
using test1.Models;

namespace test1.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
