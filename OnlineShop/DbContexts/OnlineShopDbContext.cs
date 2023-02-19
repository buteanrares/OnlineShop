using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.Data.Entities;

namespace OnlineShop.DbContexts
{
    public class OnlineShopDbContext : IdentityDbContext
    {
        public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<CustomerProductCart> CustomerProductCarts { get; set; }
        public DbSet<CustomerProductWishlist> CustomerProductWishlists { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Order>()
                .Property(order => order.OrderStatus)
                .HasConversion(new EnumToStringConverter<OrderStatus>());

            modelBuilder
                .Entity<CustomerProductCart>()
                .HasKey(cp => new { cp.CustomerId, cp.ProductId });

            modelBuilder
                .Entity<CustomerProductCart>()
                .HasOne(cp => cp.Customer)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(cp => cp.CustomerId);

            modelBuilder
                .Entity<CustomerProductCart>()
                .HasOne(cp => cp.Product)
                .WithMany(c => c.CartCustomers)
                .HasForeignKey(cp => cp.ProductId);

            modelBuilder
                .Entity<CustomerProductWishlist>()
                .HasKey(cp => new { cp.CustomerId, cp.ProductId });

            modelBuilder
                .Entity<CustomerProductWishlist>()
                .HasOne(cp => cp.Customer)
                .WithMany(c => c.WishlistProducts)
                .HasForeignKey(cp => cp.CustomerId);

            modelBuilder
                .Entity<CustomerProductWishlist>()
                .HasOne(cp => cp.Product)
                .WithMany(c => c.WishlistCustomers)
                .HasForeignKey(cp => cp.ProductId);
        }
    }
}
