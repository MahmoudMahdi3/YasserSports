using Microsoft.EntityFrameworkCore;
using YasserSports.Models;

namespace YasserSports.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options ) : base( options ) 
        {
        }
        public DbSet<Product> Products { get; set; }
            
    }
}
