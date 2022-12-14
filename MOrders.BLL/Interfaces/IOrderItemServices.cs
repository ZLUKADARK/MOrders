using MOrders.Domain.DataTransferObject.OrderItem;
using MOrders.Domain.Models;

namespace MOrders.BLL.Interfaces
{
    public interface IOrderItemServices : IServices<OrderItemDTO>
    {
        public Task<IEnumerable<OrderTableDTO>> GetOrderItemsTable(OrderFilter filter);
    }
}
