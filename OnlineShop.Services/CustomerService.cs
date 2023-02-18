using OnlineShop.Data.Entities;
using OnlineShop.DbContexts;

namespace OnlineShop.Services
{
    public class CustomerService : IService<Customer>
    {
        private readonly OnlineShopDbContext _context;

        public CustomerService(OnlineShopDbContext context)
        {
            _context = context;
        }

        public Customer Create(Customer entity)
        {
            _context.Customers.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(Customer entity)
        {
            _context.Customers.Remove(entity);
            _context.SaveChanges();
        }

        public Customer? Read(Customer entity)
        {
            return _context.Customers.Find(entity);
        }

        public Customer Update(Customer entity)
        {
            _context.Customers.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
