using MOrders.Domain.DataTransferObject.Order;
using MOrders.Domain.Models;

namespace MOrders.BLL.Interfaces
{
    public interface IOrderServices : IServices<OrderDTO>
    {
        public Task<DistinctValues> GetDistinct();
        public Task<OrderDetailWithItemsDTO> GetOrderWithItems(int id);
    }
}
