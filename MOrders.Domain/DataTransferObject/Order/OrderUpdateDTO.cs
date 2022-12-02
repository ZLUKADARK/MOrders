using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOrders.Domain.DataTransferObject.Order
{
    public class OrderUpdateDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Номер заказа")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Заказчик")]
        public int ProviderId { get; set; }
    }
}
