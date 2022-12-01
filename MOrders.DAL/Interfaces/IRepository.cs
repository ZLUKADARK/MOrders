namespace MOrders.DAL.Interfaces
{
    public interface IRepository<T, F> where T : class where F : class
    {
        public Task<T> Create(T item);
        public Task<IEnumerable<T>> GetByFilter(F filters);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Get(int id);
        public Task<bool> Delete(int id);
        public Task<bool> Update(int id, T item);
    }

    public interface IRepository<T> where T : class
    {
        public Task<T> Create(T item);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Get(int id);
        public Task<bool> Delete(int id);
        public Task<bool> Update(int id, T item);
    }
}
