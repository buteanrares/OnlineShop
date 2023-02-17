using OnlineShop.Data.Entities;
using OnlineShop.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services
{
    internal class ProductService
    {
        private readonly OnlineShopDbContext _context;

        public ProductService(OnlineShopDbContext context)
        {
            _context = context;
        }

        public Product Create(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(Product entity)
        {
            _context.Products.Remove(entity);
            _context.SaveChanges();
        }

        public Product? Read(Product entity)
        {
            return _context.Products.Find(entity);
        }

        public Product Update(Product entity)
        {
            _context.Products.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
