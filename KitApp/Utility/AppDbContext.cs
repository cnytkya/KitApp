using KitApp.Models;
using Microsoft.EntityFrameworkCore;
 
namespace KitApp.Utility
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BookType> BookTypes { get; set; }
    }
}
