using firstMVC.Models;
using Microsoft.EntityFrameworkCore;
namespace firstMVC.Data
{
    public class appDbContext(DbContextOptions < appDbContext > options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; }

    }
}
