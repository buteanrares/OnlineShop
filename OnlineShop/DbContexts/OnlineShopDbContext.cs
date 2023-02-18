using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.Data.Entities;

namespace OnlineShop.DbContexts
{
    public class OnlineShopDbContext : DbContext
    {
        public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Order>()
                .Property(order => order.OrderStatus)
                .HasConversion(new EnumToStringConverter<OrderStatus>());
        }
    }
}
