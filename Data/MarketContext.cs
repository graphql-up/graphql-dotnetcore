using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Data
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions options) : base(options) { }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>().ForNpgsqlUseXminAsConcurrencyToken();
        //    base.OnModelCreating(modelBuilder);
        //}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
