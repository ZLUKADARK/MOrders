namespace MOrders.BLL.DataTransferObject.Order
{
    public class OrderItemShorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}
