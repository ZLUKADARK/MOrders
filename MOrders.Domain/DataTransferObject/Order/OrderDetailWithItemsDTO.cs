using MOrders.Domain.DataTransferObject.Abstract;
using MOrders.Domain.DataTransferObject.OrderItem;

namespace MOrders.Domain.DataTransferObject.Order
{
    public class OrderDetailWithItemsDTO : OrderBase
    {
        public string ProviderName { get; set; }
        public List<OrderItemShortDTO> OrderItem { get; set; }
    }
}
