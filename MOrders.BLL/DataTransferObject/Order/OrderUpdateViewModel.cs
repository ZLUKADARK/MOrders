using System;
using System.ComponentModel.DataAnnotations;

namespace MOrders.BLL.DataTransferObject.Order
{
    public class OrderUpdateViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
    }
}
