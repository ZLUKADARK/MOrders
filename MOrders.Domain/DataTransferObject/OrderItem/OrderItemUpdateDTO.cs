namespace MOrders.Domain.DataTransferObject.OrderItem
{
    public class OrderItemUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}
