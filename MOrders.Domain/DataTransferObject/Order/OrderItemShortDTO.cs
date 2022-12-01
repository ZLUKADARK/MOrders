namespace MOrders.Domain.DataTransferObject.Order
{
    public class OrderItemShortDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}
