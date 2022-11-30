using Microsoft.EntityFrameworkCore;
using MOrders.DAL.Entities;

namespace MOrders.DAL.Data
{
    public class MOrdersContext : DbContext
    {
        public MOrdersContext(DbContextOptions<MOrdersContext> options)
            : base(options)
        {
        }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasIndex(p => new { p.Number, p.ProviderId }).IsUnique();

            modelBuilder.Entity<Provider>()
                .HasData(
                new Provider { Id = 1, Name = "Ozon" },
                new Provider { Id = 2, Name = "AliExpress" },
                new Provider { Id = 3, Name = "Amazon" },
                new Provider { Id = 4, Name = "Ebay" },
                new Provider { Id = 5, Name = "Lite-Computer Store" },
                new Provider { Id = 6, Name = "Cheap Wholesales" });
        }
    }
}
