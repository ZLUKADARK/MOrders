using MOrders.Domain.DataTransferObject.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MOrders.Domain.DataTransferObject.OrderItem
{
    public class OrderItemDTO : OrderItemBase
    {
        public string? OrderNumber { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int OrderId { get; set; }
    }
}
