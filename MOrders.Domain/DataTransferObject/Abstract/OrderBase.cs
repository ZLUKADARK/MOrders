using System.ComponentModel.DataAnnotations;

namespace MOrders.Domain.DataTransferObject.Abstract
{
    public abstract class OrderBase : Base
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Номер")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Поставщик")]
        public int ProviderId { get; set; }
    }
}
