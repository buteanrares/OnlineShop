using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Data.Entities;


namespace OnlineShop.Data.Persistence
{
    public static class DbInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var context = new OnlineShopDbContext(serviceProvider.GetRequiredService<DbContextOptions<OnlineShopDbContext>>());

            if (context.Products.Any())
                return;

            context.Products.Add(new Product()
            {
                Name = "test-name",
                Price = 115.23,
                Stock = 5,
                Available = true
            });

            context.SaveChanges();
        }
    }
}
