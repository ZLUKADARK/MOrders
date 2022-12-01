using System;

namespace MOrders.Domain.DataTransferObject.Order
{
    public class OrderTableDTO
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
    }
}
