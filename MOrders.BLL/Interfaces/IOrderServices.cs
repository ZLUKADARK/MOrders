using MOrders.BLL.DataTransferObject;
using MOrders.BLL.DataTransferObject.Order;
using MOrders.BLL.DataTransferObject.OrderItem;
using Orders.ViewModels.Provider;

namespace MOrders.BLL.Interfaces
{
    public interface IOrderServices
    {
        public Task<OrderViewModel> CreateOrder(OrderViewModel orders);
        public Task<OrderItemViewModel> CreateOrderItemToOrder(OrderItemViewModel orderItem);
        public Task<IEnumerable<OrderTableViewModel>> GetOrdersTable(OrdersFilterViewModel filter);
        public Task<DistinctValuesForSelect> GetDistinct();
        public Task<IEnumerable<OrderViewModel>> GetOrders();
        public Task<OrderViewModel> GetOrder(int id);
        public Task<OrderDetailWithItemsViewModel> GetOrderWithItems(int id);
        public Task<OrderItemViewModel> GetOrderItem(int id);
        public Task<IEnumerable<ProviderViewModel>> GetProviders();
        public Task<bool> DeleteOrderItem(int id);
        public Task<bool> DeleteOrder(int id);
        public Task<bool> UpdateOrder(int id, OrderUpdateViewModel orders);
        public Task<bool> UpdateOrderItem(int id, OrderItemUpdateViewModel orderItem);
    }
}
