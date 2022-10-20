using Microsoft.EntityFrameworkCore;
using onlineshopping.Models;

namespace onlineshopping.Srvices.Entites
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        public DbSet<Student> Students { get; set; }
    }
}
