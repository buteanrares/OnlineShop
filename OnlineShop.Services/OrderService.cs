using OnlineShop.Data.Entities;
using OnlineShop.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services
{
    public class OrderService
    {
        private readonly OnlineShopDbContext _context;

        public OrderService(OnlineShopDbContext context)
        {
            _context = context;
        }

        public Order Create(Order entity)
        {
            _context.Orders.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(Order entity)
        {
            _context.Orders.Remove(entity);
            _context.SaveChanges();
        }

        public Order? Read(Order entity)
        {
            return _context.Orders.Find(entity);
        }

        public Order Update(Order entity)
        {
            _context.Orders.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
