using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MOrders.Domain.DataTransferObject.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Номер")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        public string? ProviderName { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Поставщик")]
        public int ProviderId { get; set; }
    }
}
