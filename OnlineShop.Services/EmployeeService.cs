using OnlineShop.Data.Entities;
using OnlineShop.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services
{
    public class EmployeeService
    {
        private readonly OnlineShopDbContext _context;

        public EmployeeService(OnlineShopDbContext context)
        {
            _context = context;
        }

        public Employee Create(Employee entity)
        {
            _context.Employees.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(Employee entity)
        {
            _context.Employees.Remove(entity);
            _context.SaveChanges();
        }

        public Employee? Read(Employee entity)
        {
            return _context.Employees.Find(entity);
        }

        public Employee Update(Employee entity)
        {
            _context.Employees.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
