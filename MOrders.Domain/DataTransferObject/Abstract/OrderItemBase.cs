using System.ComponentModel.DataAnnotations;

namespace MOrders.Domain.DataTransferObject.Abstract
{
    public abstract class OrderItemBase : Base
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public decimal Quantity { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Unit { get; set; }

    }
}
