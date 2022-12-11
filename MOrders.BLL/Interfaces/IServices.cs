using MOrders.Domain.DataTransferObject.Order;
using MOrders.Domain.Models;

namespace MOrders.BLL.Interfaces
{
    public interface IServices<T> where T : class
    {
        public Task<T> Create(T item);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Get(int id);
        public Task<bool> Delete(int id);
        public Task<bool> Update(int id, T item);
    }
}
