using System.ComponentModel.DataAnnotations.Schema;

namespace MOrders.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; }
        [Column(TypeName = "datetime2(7)")]
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public List<OrderItem> OrderItem { get; set; }
    }
}
