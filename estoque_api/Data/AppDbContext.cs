using Microsoft.EntityFrameworkCore;
using storage.Models;

namespace storage{
    public class AppDbContext : DbContext
    {
        //table representation
        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite(connectionString:"DataSource=app.db;Cache=Shared");
        }
    }
}