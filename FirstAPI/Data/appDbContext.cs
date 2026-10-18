using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<FirstAPI.Entities.Employee> Employees { get; set; }
        public DbSet<FirstAPI.Entities.User> Users { get; set; }
        public object AccountUsers { get; internal set; }
    }
}
