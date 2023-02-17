using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Repositories
{
    public interface IRepository<T> where T : class
    {
        public T Create(T entity);
        public T Read(T entity);
        public T Update(T entity);
        public void Delete(T entity);
    }
}
