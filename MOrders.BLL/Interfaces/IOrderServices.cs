using MOrders.Domain.DataTransferObject.Order;
using MOrders.Domain.DataTransferObject.OrderItem;
using MOrders.Domain.DataTransferObject.Provider;
using MOrders.Domain.Models;

namespace MOrders.BLL.Interfaces
{
    public interface IOrderServices
    {
        public Task<OrderDTO> CreateOrder(OrderDTO orders);
        public Task<OrderItemDTO> CreateOrderItemToOrder(OrderItemDTO orderItem);
        public Task<IEnumerable<OrderTableDTO>> GetOrdersTable(OrderFilter filter);
        public Task<DistinctValues> GetDistinct();
        public Task<IEnumerable<OrderDTO>> GetOrders();
        public Task<OrderDTO> GetOrder(int id);
        public Task<OrderDetailWithItemsDTO> GetOrderWithItems(int id);
        public Task<OrderItemDTO> GetOrderItem(int id);
        public Task<IEnumerable<ProviderDTO>> GetProviders();
        public Task<bool> DeleteOrderItem(int id);
        public Task<bool> DeleteOrder(int id);
        public Task<bool> UpdateOrder(int id, OrderUpdateDTO orders);
        public Task<bool> UpdateOrderItem(int id, OrderItemUpdateDTO orderItem);
    }
}
