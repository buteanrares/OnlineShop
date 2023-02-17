namespace OnlineShop.Services
{
    public interface IService<T> where T : class
    {
        public T Create(T entity);
        public T? Read(T entity);
        public T Update(T entity);
        public void Delete(T entity);
    }
}
