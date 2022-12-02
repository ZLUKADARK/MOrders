using MOrders.Domain.Entities;
using MOrders.Domain.Models;

namespace MOrders.DAL.Interfaces
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        public Task<IEnumerable<OrderItem>> GetByFilter(OrderFilter filters);
    }
}
