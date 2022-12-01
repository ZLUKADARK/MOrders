using System;
using System.Collections.Generic;

namespace MOrders.BLL.DataTransferObject.Order
{
    public class OrderDetailWithItemsViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string ProviderName { get; set; }
        public int ProviderId { get; set; }
        public List<OrderItemShorViewModel> OrderItem { get; set; }
    }
}
