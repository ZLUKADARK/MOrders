using System.ComponentModel.DataAnnotations.Schema;

namespace MOrders.Domain.Entities
{
    public class Provider
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}
