using Common;
using Microsoft.EntityFrameworkCore;

namespace MusalovKR
{
    public class AppDbContext : DbContext
    {
        public DbSet<Posts> Posts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Posts>().HasKey(a => a.ID_Posts);
        }
    }
}
