using System.ComponentModel.DataAnnotations;

namespace MOrders.Domain.DataTransferObject.OrderItem
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public decimal Quantity { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Unit { get; set; }
        public string? OrderNumber { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int OrderId { get; set; }
    }
}
