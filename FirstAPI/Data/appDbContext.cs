using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Data
{
    public class appDbContext : DbContext
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options)
        {
            
        }
        public DbSet<FirstAPI.Entities.Employee> Employees { get; set; }
        public DbSet<FirstAPI.Entities.User> Users { get; set; }
    }
}
