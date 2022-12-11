using MOrders.Domain.DataTransferObject.Abstract;

namespace MOrders.Domain.DataTransferObject.Order
{
    public class OrderTableDTO : OrderItemBase
    {
        public int OrderId { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
    }
}
